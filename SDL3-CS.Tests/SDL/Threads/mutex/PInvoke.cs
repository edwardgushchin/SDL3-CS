using System.Threading;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Threads.Mutex;

internal static class PInvokeTests
{
    public static void RunAll()
    {
        Mutex_CreateLockTryUnlockDestroy_Works();
        RWLock_CreateReadWriteTryUnlockDestroy_Works();
        Semaphore_WaitSignalValueDestroy_Works();
        Condition_SignalBroadcastWaitAndTimeoutDestroy_Works();
        InitState_TransitionsWithShouldInitShouldQuitAndSetInitialized();
    }

    public static void Mutex_CreateLockTryUnlockDestroy_Works()
    {
        IntPtr mutex = SDL3.SDL.CreateMutex();
        int lockCount = 0;

        TestAssert.True(mutex != IntPtr.Zero, "SDL.CreateMutex must return a mutex handle.");

        try
        {
            SDL3.SDL.LockMutex(mutex);
            lockCount++;

            bool recursiveLock = SDL3.SDL.TryLockMutex(mutex);
            if (recursiveLock)
            {
                lockCount++;
            }

            TestAssert.Equal(true, recursiveLock, "SDL.TryLockMutex must allow recursive locking for SDL mutexes.");
            TestAssert.Equal(true, SDL3.SDL.TryLockMutex(IntPtr.Zero), "SDL.TryLockMutex must succeed for a null mutex.");
        }
        finally
        {
            while (lockCount > 0)
            {
                SDL3.SDL.UnlockMutex(mutex);
                lockCount--;
            }

            SDL3.SDL.DestroyMutex(mutex);
        }
    }

    public static void RWLock_CreateReadWriteTryUnlockDestroy_Works()
    {
        IntPtr rwlock = SDL3.SDL.CreateRWLock();
        int readLockCount = 0;
        bool writeLocked = false;

        TestAssert.True(rwlock != IntPtr.Zero, "SDL.CreateRWLock must return a read/write lock handle.");

        try
        {
            SDL3.SDL.LockRWLockForReading(rwlock);
            readLockCount++;

            bool recursiveRead = SDL3.SDL.TryLockRWLockForReading(rwlock);
            if (recursiveRead)
            {
                readLockCount++;
            }

            TestAssert.Equal(true, recursiveRead, "SDL.TryLockRWLockForReading must allow a second read lock.");

            while (readLockCount > 0)
            {
                SDL3.SDL.UnlockRWLock(rwlock);
                readLockCount--;
            }

            bool tryWrite = SDL3.SDL.TryLockRWLockForWriting(rwlock);
            if (tryWrite)
            {
                writeLocked = true;
            }

            TestAssert.Equal(true, tryWrite, "SDL.TryLockRWLockForWriting must lock an unlocked rwlock.");

            SDL3.SDL.UnlockRWLock(rwlock);
            writeLocked = false;

            SDL3.SDL.LockRWLockForWriting(rwlock);
            writeLocked = true;
        }
        finally
        {
            if (writeLocked)
            {
                SDL3.SDL.UnlockRWLock(rwlock);
            }

            while (readLockCount > 0)
            {
                SDL3.SDL.UnlockRWLock(rwlock);
                readLockCount--;
            }

            SDL3.SDL.DestroyRWLock(rwlock);
        }
    }

    public static void Semaphore_WaitSignalValueDestroy_Works()
    {
        IntPtr semaphore = SDL3.SDL.CreateSemaphore(1);

        TestAssert.True(semaphore != IntPtr.Zero, "SDL.CreateSemaphore must return a semaphore handle.");

        try
        {
            TestAssert.Equal(1u, SDL3.SDL.GetSemaphoreValue(semaphore), "SDL.GetSemaphoreValue must report the initial value.");
            TestAssert.Equal(true, SDL3.SDL.TryWaitSemaphore(semaphore), "SDL.TryWaitSemaphore must consume an available value.");
            TestAssert.Equal(0u, SDL3.SDL.GetSemaphoreValue(semaphore), "SDL.TryWaitSemaphore must decrement the semaphore.");
            TestAssert.Equal(false, SDL3.SDL.TryWaitSemaphore(semaphore), "SDL.TryWaitSemaphore must fail when the value is zero.");
            TestAssert.Equal(false, SDL3.SDL.WaitSemaphoreTimeout(semaphore, 0), "SDL.WaitSemaphoreTimeout must time out when the value is zero.");

            SDL3.SDL.SignalSemaphore(semaphore);
            TestAssert.Equal(1u, SDL3.SDL.GetSemaphoreValue(semaphore), "SDL.SignalSemaphore must increment the semaphore.");
            TestAssert.Equal(true, SDL3.SDL.WaitSemaphoreTimeout(semaphore, 0), "SDL.WaitSemaphoreTimeout must consume an available value.");
            TestAssert.Equal(0u, SDL3.SDL.GetSemaphoreValue(semaphore), "SDL.WaitSemaphoreTimeout must decrement the semaphore.");

            SDL3.SDL.SignalSemaphore(semaphore);
            SDL3.SDL.WaitSemaphore(semaphore);
            TestAssert.Equal(0u, SDL3.SDL.GetSemaphoreValue(semaphore), "SDL.WaitSemaphore must consume an available value.");
        }
        finally
        {
            SDL3.SDL.DestroySemaphore(semaphore);
        }
    }

    public static void Condition_SignalBroadcastWaitAndTimeoutDestroy_Works()
    {
        IntPtr mutex = SDL3.SDL.CreateMutex();
        IntPtr condition = SDL3.SDL.CreateCondition();
        bool mutexLocked = false;

        TestAssert.True(mutex != IntPtr.Zero, "SDL.CreateMutex must return a mutex for condition tests.");
        TestAssert.True(condition != IntPtr.Zero, "SDL.CreateCondition must return a condition handle.");

        try
        {
            SDL3.SDL.SignalCondition(condition);
            SDL3.SDL.BroadcastCondition(condition);

            SDL3.SDL.LockMutex(mutex);
            mutexLocked = true;

            bool timedOut = SDL3.SDL.WaitConditionTimeout(condition, mutex, 0);

            TestAssert.Equal(false, timedOut, "SDL.WaitConditionTimeout must time out when there is no signal.");

            SDL3.SDL.UnlockMutex(mutex);
            mutexLocked = false;

            WaitConditionUntilSignaled(condition, mutex);
        }
        finally
        {
            if (mutexLocked)
            {
                SDL3.SDL.UnlockMutex(mutex);
            }

            SDL3.SDL.DestroyCondition(condition);
            SDL3.SDL.DestroyMutex(mutex);
        }
    }

    public static void InitState_TransitionsWithShouldInitShouldQuitAndSetInitialized()
    {
        SDL3.SDL.InitState state = default;

        bool shouldInit = SDL3.SDL.ShouldInit(ref state);

        TestAssert.Equal(true, shouldInit, "SDL.ShouldInit must start initialization for a default state.");
        TestAssert.Equal((int)SDL3.SDL.InitStatus.Initializing, state.status.Value, "SDL.ShouldInit must mark the state as initializing.");

        SDL3.SDL.SetInitialized(ref state, true);

        TestAssert.Equal((int)SDL3.SDL.InitStatus.Initialized, state.status.Value, "SDL.SetInitialized(true) must mark the state as initialized.");
        TestAssert.Equal(false, SDL3.SDL.ShouldInit(ref state), "SDL.ShouldInit must return false for an initialized state.");
        TestAssert.Equal(true, SDL3.SDL.ShouldQuit(ref state), "SDL.ShouldQuit must start cleanup for an initialized state.");
        TestAssert.Equal((int)SDL3.SDL.InitStatus.UnInitializing, state.status.Value, "SDL.ShouldQuit must mark the state as uninitializing.");

        SDL3.SDL.SetInitialized(ref state, false);

        TestAssert.Equal((int)SDL3.SDL.InitStatus.UnInitialized, state.status.Value, "SDL.SetInitialized(false) must mark the state as uninitialized.");
        TestAssert.Equal(false, SDL3.SDL.ShouldQuit(ref state), "SDL.ShouldQuit must return false for an uninitialized state.");
    }

    private static void WaitConditionUntilSignaled(IntPtr condition, IntPtr mutex)
    {
        bool signaled = false;
        Exception? signalerException = null;

        SDL3.SDL.LockMutex(mutex);
        bool mutexLocked = true;

        System.Threading.Thread signaler = new(() =>
        {
            try
            {
                System.Threading.Thread.Sleep(25);
                SDL3.SDL.LockMutex(mutex);
                try
                {
                    signaled = true;
                    SDL3.SDL.SignalCondition(condition);
                }
                finally
                {
                    SDL3.SDL.UnlockMutex(mutex);
                }
            }
            catch (Exception ex)
            {
                signalerException = ex;
            }
        });

        signaler.Start();

        try
        {
            while (!signaled)
            {
                SDL3.SDL.WaitCondition(condition, mutex);
            }
        }
        finally
        {
            if (mutexLocked)
            {
                SDL3.SDL.UnlockMutex(mutex);
                mutexLocked = false;
            }
        }

        bool joined = signaler.Join(5_000);

        TestAssert.Equal(true, joined, "The condition signaler thread must complete.");
        TestAssert.True(signalerException is null, $"The condition signaler thread must not fail: {signalerException}");
        TestAssert.Equal(true, signaled, "SDL.WaitCondition must return after the condition is signaled.");
    }
}
