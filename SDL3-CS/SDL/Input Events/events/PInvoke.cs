#region License
/* Copyright (c) 2024-2026 Eduard Gushchin.
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 */
#endregion

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL3;

public static partial class SDL
{
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PumpEvents"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PumpEvents();
    private delegate void PumpEventsNativeDelegate();
    private static PumpEventsNativeDelegate PumpEventsNativeFunction = SDL_PumpEvents;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_PumpEvents(void);</code>
    /// <summary>
    /// <para>Pump the event loop, gathering events from the input devices.</para>
    /// <para>This function updates the event queue and internal input device state.</para>
    /// <para><see cref="PumpEvents"/> gathers all the pending input information from devices and
    /// places it in the event queue. Without calls to <see cref="PumpEvents"/> no events
    /// would ever be placed on the queue. Often the need for calls to
    /// <see cref="PumpEvents"/> is hidden from the user since <see cref="PollEvent(out Event)"/> and
    /// <see cref="WaitEvent"/> implicitly call <see cref="PumpEvents"/>. However, if you are not
    /// polling or waiting for events (e.g. you are filtering them), then you must
    /// call <see cref="PumpEvents"/> to force an event queue update.</para>
    /// </summary>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PollEvent(out Event)"/>
    /// <seealso cref="WaitEvent"/>
    public static void PumpEvents()
    {
        PumpEventsNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PeepEvents"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_PeepEvents(IntPtr events, int numevents, EventAction action, uint minType, uint maxType);
    private delegate int PeepEventsPointerNativeDelegate(IntPtr events, int numevents, EventAction action, uint minType, uint maxType);
    private static PeepEventsPointerNativeDelegate PeepEventsPointerNativeFunction = SDL_PeepEvents;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_PeepEvents(SDL_Event *events, int numevents, SDL_EventAction action, Uint32 minType, Uint32 maxType);</code>
    /// <summary>
    /// <para>Check the event queue for messages and optionally return them.</para>
    /// <para><c>action</c> may be any of the following:</para>
    /// <list type="bullet">
    /// <item><see cref="EventAction.AddEvent"/>: up to <c>numevents</c> events will be added to the back of the
    /// event queue.</item>
    /// <item><see cref="EventAction.PeekEvent"/>: <c>numevents</c> events at the front of the event queue,
    /// within the specified minimum and maximum type, will be returned to the
    /// caller and will <b>not</b> be removed from the queue. If you pass <c>null</c> for</item>
    /// <item><c>events</c>, then <c>numevents</c> is ignored and the total number of matching
    /// events will be returned.</item>
    /// <item><see cref="EventAction.GetEvent"/>: up to <c>numevents</c> events at the front of the event queue,
    /// within the specified minimum and maximum type, will be returned to the
    /// caller and will be removed from the queue.</item>
    /// </list>
    /// <para>You may have to call <see cref="PumpEvents"/> before calling this function.
    /// Otherwise, the events may not be ready to be filtered when you call
    /// <see cref="PeepEvents(nint, int, EventAction, uint, uint)"/>.</para>
    /// </summary>
    /// <param name="events">destination buffer for the retrieved events, may be <c>null</c> to
    /// leave the events in the queue and return the number of events
    /// that would have been stored.</param>
    /// <param name="numevents">if action is <see cref="EventAction.AddEvent"/>, the number of events to add
    /// back to the event queue; if action is <see cref="EventAction.PeekEvent"/> or
    /// <see cref="EventAction.GetEvent"/>, the maximum number of events to retrieve.</param>
    /// <param name="action">action to take; see Remarks for details.</param>
    /// <param name="minType">minimum value of the event type to be considered;
    /// <see cref="EventType.First"/> is a safe choice.</param>
    /// <param name="maxType">maximum value of the event type to be considered;
    /// <see cref="EventType.Last"/> is a safe choice.</param>
    /// <returns>the number of events actually stored or -1 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PollEvent(out Event)"/>
    /// <seealso cref="PumpEvents"/>
    /// <seealso cref="PushEvent"/>
    public static int PeepEvents(IntPtr events, int numevents, EventAction action, uint minType, uint maxType)
    {
        return PeepEventsPointerNativeFunction(events, numevents, action, minType, maxType);
    }


    [ExcludeFromCodeCoverage]
    [DllImport(SDLLibrary, EntryPoint = "SDL_PeepEvents"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static extern int SDL_PeepEvents([Out] Event[] events, int numevents, EventAction action, uint minType, uint maxType);
    private delegate int PeepEventsArrayNativeDelegate(Event[] events, int numevents, EventAction action, uint minType, uint maxType);
    private static PeepEventsArrayNativeDelegate PeepEventsArrayNativeFunction = SDL_PeepEvents;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_PeepEvents(SDL_Event *events, int numevents, SDL_EventAction action, Uint32 minType, Uint32 maxType);</code>
    /// <summary>
    /// <para>Check the event queue for messages and optionally return them.</para>
    /// <para><c>action</c> may be any of the following:</para>
    /// <list type="bullet">
    /// <item><see cref="EventAction.AddEvent"/>: up to <c>numevents</c> events will be added to the back of the
    /// event queue.</item>
    /// <item><see cref="EventAction.PeekEvent"/>: <c>numevents</c> events at the front of the event queue,
    /// within the specified minimum and maximum type, will be returned to the
    /// caller and will <b>not</b> be removed from the queue. If you pass <c>null</c> for</item>
    /// <item><c>events</c>, then <c>numevents</c> is ignored and the total number of matching
    /// events will be returned.</item>
    /// <item><see cref="EventAction.GetEvent"/>: up to <c>numevents</c> events at the front of the event queue,
    /// within the specified minimum and maximum type, will be returned to the
    /// caller and will be removed from the queue.</item>
    /// </list>
    /// <para>You may have to call <see cref="PumpEvents"/> before calling this function.
    /// Otherwise, the events may not be ready to be filtered when you call
    /// <see cref="PeepEvents(nint, int, EventAction, uint, uint)"/>.</para>
    /// </summary>
    /// <param name="events">destination buffer for the retrieved events, may be <c>null</c> to
    /// leave the events in the queue and return the number of events
    /// that would have been stored.</param>
    /// <param name="numevents">if action is <see cref="EventAction.AddEvent"/>, the number of events to add
    /// back to the event queue; if action is <see cref="EventAction.PeekEvent"/> or
    /// <see cref="EventAction.GetEvent"/>, the maximum number of events to retrieve.</param>
    /// <param name="action">action to take; see [[#action|Remarks]] for details.</param>
    /// <param name="minType">minimum value of the event type to be considered;
    /// <see cref="EventType.First"/> is a safe choice.</param>
    /// <param name="maxType">maximum value of the event type to be considered;
    /// <see cref="EventType.Last"/> is a safe choice.</param>
    /// <returns>the number of events actually stored or -1 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PollEvent(out Event)"/>
    /// <seealso cref="PumpEvents"/>
    /// <seealso cref="PushEvent"/>
    public static int PeepEvents([Out] Event[] events, int numevents, EventAction action, uint minType, uint maxType)
    {
        return PeepEventsArrayNativeFunction(events, numevents, action, minType, maxType);
    }

    /// <inheritdoc cref="PeepEvents(Event[], int, EventAction, uint, uint)"/>
    public static unsafe int PeepEvents(Span<Event> events, int numevents, EventAction action, uint minType, uint maxType)
    {
        fixed (Event* eventsPtr = events)
        {
            return PeepEventsPointerNativeFunction((IntPtr)eventsPtr, numevents, action, minType, maxType);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasEvent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasEvent(uint type);
    private delegate bool HasEventNativeDelegate(uint type);
    private static HasEventNativeDelegate HasEventNativeFunction = SDL_HasEvent;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasEvent(Uint32 type);</code>
    /// <summary>
    /// <para>Check for the existence of a certain event type in the event queue.</para>
    /// <para>If you need to check for a range of event types, use <see cref="HasEvents"/>
    /// instead.</para>
    /// </summary>
    /// <param name="type">the type of event to be queried; see <see cref="EventType"/> for details.</param>
    /// <returns><c>true</c> if events matching <c>type</c> are present, or <c>false</c> if events
    /// matching <c>type</c> are not present.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasEvents"/>
    public static bool HasEvent(uint type)
    {
        return HasEventNativeFunction(type);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasEvents"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasEvents(uint minType, uint maxType);
    private delegate bool HasEventsNativeDelegate(uint minType, uint maxType);
    private static HasEventsNativeDelegate HasEventsNativeFunction = SDL_HasEvents;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasEvents(Uint32 minType, Uint32 maxType);</code>
    /// <summary>
    /// <para>Check for the existence of certain event types in the event queue.</para>
    /// <para>If you need to check for a single event type, use <see cref="HasEvent"/> instead.</para>
    /// </summary>
    /// <param name="minType">the low end of event type to be queried, inclusive; see
    /// <see cref="EventType"/> for details.</param>
    /// <param name="maxType">the high end of event type to be queried, inclusive; see
    /// <see cref="EventType"/> for details.</param>
    /// <returns><c>true</c> if events with type >= <c>minType</c> and &lt;= <c>maxType</c> are
    /// present, or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasEvent"/>
    public static bool HasEvents(uint minType, uint maxType)
    {
        return HasEventsNativeFunction(minType, maxType);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_FlushEvent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_FlushEvent(uint type);
    private delegate void FlushEventNativeDelegate(uint type);
    private static FlushEventNativeDelegate FlushEventNativeFunction = SDL_FlushEvent;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_FlushEvent(Uint32 type);</code>
    /// <summary>
    /// <para>Clear events of a specific type from the event queue.</para>
    /// <para>This will unconditionally remove any events from the queue that match
    /// <c>type</c>. If you need to remove a range of event types, use <see cref="FlushEvents"/>
    /// instead.</para>
    /// <para>It's also normal to just ignore events you don't care about in your event
    /// loop without calling this function.</para>
    /// <para>This function only affects currently queued events. If you want to make
    /// sure that all pending OS events are flushed, you can call <see cref="PumpEvents"/>
    /// on the main thread immediately before the flush call.</para>
    /// <para>If you have user events with custom data that needs to be freed, you should
    /// use <see cref="PeepEvents(nint, int, EventAction, uint, uint)"/> to remove and clean up those events before calling
    /// this function.</para>
    /// </summary>
    /// <param name="type">the type of event to be cleared; see <see cref="EventType"/> for details.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="FlushEvents"/>
    public static void FlushEvent(uint type)
    {
        FlushEventNativeFunction(type);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_FlushEvents"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_FlushEvents(uint minType, uint maxType);
    private delegate void FlushEventsNativeDelegate(uint minType, uint maxType);
    private static FlushEventsNativeDelegate FlushEventsNativeFunction = SDL_FlushEvents;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_FlushEvents(Uint32 minType, Uint32 maxType);</code>
    /// <summary>
    /// <para>Clear events of a range of types from the event queue.</para>
    /// <para>This will unconditionally remove any events from the queue that are in the
    /// range of <c>minType</c> to <c>maxType</c>, inclusive. If you need to remove a single
    /// event type, use <see cref="FlushEvent"/> instead.</para>
    /// <para>It's also normal to just ignore events you don't care about in your event
    /// loop without calling this function.</para>
    /// <para>This function only affects currently queued events. If you want to make
    /// sure that all pending OS events are flushed, you can call <see cref="PumpEvents"/>
    /// on the main thread immediately before the flush call.</para>
    /// </summary>
    /// <param name="minType">the low end of event type to be cleared, inclusive; see
    /// <see cref="EventType"/> for details.</param>
    /// <param name="maxType">the high end of event type to be cleared, inclusive; see
    /// <see cref="EventType"/> for details.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="FlushEvent"/>
    public static void FlushEvents(uint minType, uint maxType)
    {
        FlushEventsNativeFunction(minType, maxType);
    }


    [ExcludeFromCodeCoverage]
    [DllImport(SDLLibrary, EntryPoint = "SDL_PollEvent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static extern bool SDL_PollEvent(out Event @event);
    private delegate bool PollEventOutNativeDelegate(out Event @event);
    private static PollEventOutNativeDelegate PollEventOutNativeFunction = SDL_PollEvent;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_PollEvent(SDL_Event *event);</code>
    /// <summary>
    /// <para>Poll for currently pending events.</para>
    /// <para>If <c>event</c> is not <c>null</c>, the next event is removed from the queue and stored
    /// in the <see cref="Event"/> structure pointed to by <c>event</c>.</para>
    /// <para>If <c>event</c> is <c>null</c>, it simply returns <c>true</c> if there is an event in the
    /// queue, but will not remove it from the queue.</para>
    /// <para>As this function may implicitly call <see cref="PumpEvents"/>, you can only call
    /// this function in the thread that initialized the video subsystem.</para>
    /// <para><see cref="PollEvent(out Event)"/> is the favored way of receiving system events since it can
    /// be done from the main loop and does not suspend the main loop while waiting
    /// on an event to be posted.</para>
    /// <para>The common practice is to fully process the event queue once every frame,
    /// usually as a first step before updating the game's state:</para>
    /// <code>
    /// while (game_is_still_running) {
    ///     while (SDL.PollEvent(out var e)) {  // poll until all events are handled!
    ///         // decide what to do with this event.
    ///     }
    ///
    /// // update game state, draw the current frame
    /// }
    /// </code>
    /// <para>Note that Windows (and possibly other platforms) has a quirk about how it
    /// handles events while dragging/resizing a window, which can cause this
    /// function to block for significant amounts of time. Technical explanations
    /// and solutions are discussed on the wiki:
    ///
    /// https://wiki.libsdl.org/SDL3/AppFreezeDuringDrag</para>
    /// </summary>
    /// <param name="event">the <see cref="Event"/> structure to be filled with the next event from
    /// the queue, or <c>null</c>.</param>
    /// <returns><c>true</c> if this got an event or <c>false</c> if there are none available.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PushEvent"/>
    /// <seealso cref="WaitEvent"/>
    /// <seealso cref="WaitEventTimeout"/>
    public static bool PollEvent(out Event @event)
    {
        return PollEventOutNativeFunction(out @event);
    }


    [ExcludeFromCodeCoverage]
    [DllImport(SDLLibrary, EntryPoint = "SDL_PollEvent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static extern bool SDL_PollEvent(IntPtr @event);
    private delegate bool PollEventPointerNativeDelegate(IntPtr @event);
    private static PollEventPointerNativeDelegate PollEventPointerNativeFunction = SDL_PollEvent;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_PollEvent(SDL_Event *event);</code>
    /// <summary>
    /// <para>Poll for currently pending events.</para>
    /// <para>If <c>event</c> is not <c>null</c>, the next event is removed from the queue and stored
    /// in the SDL_Event structure pointed to by `event`. The 1 returned refers to
    /// this event, immediately stored in the SDL Event structure -- not an event
    /// to follow.</para>
    /// <para>As this function may implicitly call <see cref="PumpEvents"/>, you can only call
    /// this function in the thread that set the video mode.</para>
    /// <para><see cref="PollEvent(out Event)"/> is the favored way of receiving system events since it can
    /// be done from the main loop and does not suspend the main loop while waiting
    /// on an event to be posted.</para>
    /// <para>The common practice is to fully process the event queue once every frame,
    /// usually as a first step before updating the game's state:</para>
    /// <code>
    /// while (game_is_still_running) {
    ///     while (SDL.PollEvent(out var e)) {  // poll until all events are handled!
    ///         // decide what to do with this event.
    ///     }
    ///
    /// // update game state, draw the current frame
    /// }
    /// </code>
    /// <para>Note that Windows (and possibly other platforms) has a quirk about how it
    /// handles events while dragging/resizing a window, which can cause this
    /// function to block for significant amounts of time. Technical explanations
    /// and solutions are discussed on the wiki:
    ///
    /// https://wiki.libsdl.org/SDL3/AppFreezeDuringDrag</para>
    /// </summary>
    /// <param name="event">the <see cref="Event"/> structure to be filled with the next event from
    /// the queue, or <c>null</c>.</param>
    /// <returns><c>true</c> if this got an event or <c>false</c> if there are none available.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PushEvent"/>
    /// <seealso cref="WaitEvent"/>
    /// <seealso cref="WaitEventTimeout"/>
    public static bool PollEvent(IntPtr @event)
    {
        return PollEventPointerNativeFunction(@event);
    }


    [ExcludeFromCodeCoverage]
    [DllImport(SDLLibrary, EntryPoint = "SDL_WaitEvent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static extern bool SDL_WaitEvent(out Event @event);
    private delegate bool WaitEventNativeDelegate(out Event @event);
    private static WaitEventNativeDelegate WaitEventNativeFunction = SDL_WaitEvent;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WaitEvent(SDL_Event *event);</code>
    /// <summary>
    /// <para>Wait indefinitely for the next available event.</para>
    /// <para>If <c>event</c> is not <c>null</c>, the next event is removed from the queue and stored
    /// in the <see cref="Event"/> structure pointed to by <c>event</c>.</para>
    /// <para>As this function may implicitly call <see cref="PumpEvents"/>, you can only call
    /// this function in the thread that initialized the video subsystem.</para>
    /// </summary>
    /// <param name="event">the <see cref="Event"/> structure to be filled in with the next event
    /// from the queue, or <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> if there was an error while waiting for
    /// events; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PollEvent(out Event)"/>
    /// <seealso cref="PushEvent"/>
    /// <seealso cref="WaitEventTimeout"/>
    public static bool WaitEvent(out Event @event)
    {
        return WaitEventNativeFunction(out @event);
    }


    [ExcludeFromCodeCoverage]
    [DllImport(SDLLibrary, EntryPoint = "SDL_WaitEventTimeout"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static extern bool SDL_WaitEventTimeout(out Event @event, int timeoutMs);
    private delegate bool WaitEventTimeoutNativeDelegate(out Event @event, int timeoutMs);
    private static WaitEventTimeoutNativeDelegate WaitEventTimeoutNativeFunction = SDL_WaitEventTimeout;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WaitEventTimeout(SDL_Event *event, Sint32 timeoutMS);</code>
    /// <summary>
    /// <para>Wait until the specified timeout (in milliseconds) for the next available
    /// event.</para>
    /// <para>If <c>event</c> is not <c>null</c>, the next event is removed from the queue and stored
    /// in the <seealso cref="Event"/> structure pointed to by <c>event</c>.</para>
    /// <para>As this function may implicitly call <see cref="PumpEvents"/>, you can only call
    /// this function in the thread that initialized the video subsystem.</para>
    /// <para>The timeout is not guaranteed, the actual wait time could be longer due to
    /// system scheduling.</para>
    /// </summary>
    /// <param name="event">the <see cref="Event"/> structure to be filled in with the next event
    /// from the queue, or <c>null</c>.</param>
    /// <param name="timeoutMs">the maximum number of milliseconds to wait for the next
    /// available event, or <c>-1</c> to wait indefinitely.</param>
    /// <returns><c>true</c> if this got an event or <c>false</c> if the timeout elapsed without
    /// any events available.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PollEvent(out Event)"/>
    /// <seealso cref="PushEvent"/>
    /// <seealso cref="WaitEvent"/>
    public static bool WaitEventTimeout(out Event @event, int timeoutMs)
    {
        return WaitEventTimeoutNativeFunction(out @event, timeoutMs);
    }


    [ExcludeFromCodeCoverage]
    [DllImport(SDLLibrary, EntryPoint = "SDL_PushEvent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static extern bool SDL_PushEvent(ref Event @event);
    private delegate bool PushEventNativeDelegate(ref Event @event);
    private static PushEventNativeDelegate PushEventNativeFunction = SDL_PushEvent;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_PushEvent(SDL_Event *event);</code>
    /// <summary>
    /// <para>Add an event to the event queue.</para>
    /// <para>The event queue can actually be used as a two way communication channel.
    /// Not only can events be read from the queue, but the user can also push
    /// their own events onto it. <c>event</c> is a pointer to the event structure you
    /// wish to push onto the queue. The event is copied into the queue, and the
    /// caller may dispose of the memory pointed to after <see cref="PushEvent"/> returns.</para>
    /// <para>Note: Pushing device input events onto the queue doesn't modify the state
    /// of the device within SDL.</para>
    /// <para>Note: Events pushed onto the queue with <see cref="PushEvent"/> get passed through
    /// the event filter but events added with <see cref="PeepEvents(nint, int, EventAction, uint, uint)"/> do not.</para>
    /// <para>For pushing application-specific events, please use <see cref="RegisterEvents"/> to
    /// get an event type that does not conflict with other code that also wants
    /// its own custom event types.</para>
    /// </summary>
    /// <param name="event">the <see cref="Event"/> to be added to the queue.</param>
    /// <returns><c>true</c> on success, <c>false</c> if the event was filtered or on failure;
    /// call <see cref="GetError"/> for more information. A common reason for
    /// error is the event queue being full.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PeepEvents(nint, int, EventAction, uint, uint)"/>
    /// <seealso cref="PollEvent(out Event)"/>
    /// <seealso cref="RegisterEvents"/>
    public static bool PushEvent(ref Event @event)
    {
        return PushEventNativeFunction(ref @event);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetEventFilter"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetEventFilter(EventFilter filter, IntPtr userdata);
    private delegate void SetEventFilterNativeDelegate(EventFilter filter, IntPtr userdata);
    private static SetEventFilterNativeDelegate SetEventFilterNativeFunction = SDL_SetEventFilter;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetEventFilter(SDL_EventFilter filter, void *userdata);</code>
    /// <summary>
    /// <para>Set up a filter to process all events before they are added to the internal
    /// event queue.</para>
    /// <para>If you just want to see events without modifying them or preventing them
    /// from being queued, you should use <see cref="AddEventWatch"/> instead.</para>
    /// <para>If the filter function returns <c>true</c> when called, then the event will be
    /// added to the internal queue. If it returns <c>false</c>, then the event will be
    /// dropped from the queue, but the internal state will still be updated. This
    /// allows selective filtering of dynamically arriving events.</para>
    /// <para><b>WARNING</b>: Be very careful of what you do in the event filter function,
    /// as it may run in a different thread! The exception is handling of
    /// <see cref="EventType.WindowExposed"/>, which is guaranteed to be sent from the OS on the
    /// main thread and you are expected to redraw your window in response to this
    /// event.</para>
    /// <para>On platforms that support it, if the quit event is generated by an
    /// interrupt sinal (e.g. pressing Ctrl-C), it will be delivered to the
    /// application at the next event poll.</para>
    /// <para>There is one caveat when dealing with the <see cref="QuitEvent"/> event type. The
    /// event filter is only called when the window manager desires to close the
    /// application window. If the event filter returns 1, then the window will be
    /// closed, otherwise the window will remain open if possible.</para>
    /// <para>Note: Disabled events never make it to the event filter function; see
    /// <see cref="SetEventEnabled"/>.</para>
    /// <para>Note: If you just want to inspect events without filtering, you should use
    /// <see cref="AddEventWatch"/> instead.</para>
    /// <para>Note: Events pushed onto the queue with <see cref="PushEvent"/> get passed through
    /// the event filter, but events pushed onto the queue with <see cref="PeepEvents(nint, int, EventAction, uint, uint)"/> do
    /// not.</para>
    /// </summary>
    /// <param name="filter">a function to call when an event happens.</param>
    /// <param name="userdata">a pointer that is passed to <c>filter</c>.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddEventWatch"/>
    /// <seealso cref="SetEventEnabled"/>
    /// <seealso cref="GetEventFilter"/>
    /// <seealso cref="PeepEvents(nint, int, EventAction, uint, uint)"/>
    /// <seealso cref="PushEvent"/>
    public static void SetEventFilter(EventFilter filter, IntPtr userdata)
    {
        SetEventFilterNativeFunction(filter, userdata);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetEventFilter"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetEventFilter(out EventFilter filter, out IntPtr userdata);
    private delegate bool GetEventFilterNativeDelegate(out EventFilter filter, out IntPtr userdata);
    private static GetEventFilterNativeDelegate GetEventFilterNativeFunction = SDL_GetEventFilter;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetEventFilter(SDL_EventFilter *filter, void **userdata);</code>
    /// <summary>
    /// <para>Query the current event filter.</para>
    /// <para>This function can be used to "chain" filters, by saving the existing filter
    /// before replacing it with a function that will call that saved filter.</para>
    /// </summary>
    /// <param name="filter">the current callback function will be stored here.</param>
    /// <param name="userdata">the pointer that is passed to the current event filter will
    /// be stored here.</param>
    /// <returns><c>true</c> on success or <c>false</c> if there is no event filter set.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetEventFilter"/>
    public static bool GetEventFilter(out EventFilter filter, out IntPtr userdata)
    {
        return GetEventFilterNativeFunction(out filter, out userdata);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AddEventWatch"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_AddEventWatch(EventFilter filter, IntPtr userdata);
    private delegate bool AddEventWatchNativeDelegate(EventFilter filter, IntPtr userdata);
    private static AddEventWatchNativeDelegate AddEventWatchNativeFunction = SDL_AddEventWatch;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_AddEventWatch(SDL_EventFilter filter, void *userdata);</code>
    /// <summary>
    /// <para>Add a callback to be triggered when an event is added to the event queue.</para>
    /// <para><c>filter</c> will be called when an event happens, and its return value is
    /// ignored.</para>
    /// <para><b>WARNING</b>: Be very careful of what you do in the event filter function,
    /// as it may run in a different thread!</para>
    /// <para>If the quit event is generated by a signal (e.g. SIGINT), it will bypass
    /// the internal queue and be delivered to the watch callback immediately, and
    /// arrive at the next event poll.</para>
    /// <para>Note: the callback is called for events posted by the user through
    /// <see cref="PushEvent"/>, but not for disabled events, nor for events by a filter
    /// callback set with <see cref="SetEventFilter"/>, nor for events posted by the user
    /// through <see cref="PeepEvents(nint, int, EventAction, uint, uint)"/>.</para>
    /// </summary>
    /// <param name="filter">an <see cref="EventFilter"/> function to call when an event happens.</param>
    /// <param name="userdata">a pointer that is passed to <c>filter</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RemoveEventWatch"/>
    /// <seealso cref="SetEventFilter"/>
    public static bool AddEventWatch(EventFilter filter, IntPtr userdata)
    {
        return AddEventWatchNativeFunction(filter, userdata);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RemoveEventWatch"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_RemoveEventWatch(EventFilter filter, IntPtr userdata);
    private delegate void RemoveEventWatchNativeDelegate(EventFilter filter, IntPtr userdata);
    private static RemoveEventWatchNativeDelegate RemoveEventWatchNativeFunction = SDL_RemoveEventWatch;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_RemoveEventWatch(SDL_EventFilter filter, void *userdata);</code>
    /// <summary>
    /// <para>Remove an event watch callback added with <see cref="AddEventWatch"/>.</para>
    /// <para>This function takes the same input as <see cref="AddEventWatch"/> to identify and
    /// delete the corresponding callback.</para>
    /// </summary>
    /// <param name="filter">the function originally passed to <see cref="AddEventWatch"/>.</param>
    /// <param name="userdata">the pointer originally passed to <see cref="AddEventWatch"/>.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddEventWatch"/>
    public static void RemoveEventWatch(EventFilter filter, IntPtr userdata)
    {
        RemoveEventWatchNativeFunction(filter, userdata);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_FilterEvents"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_FilterEvents(EventFilter filter, IntPtr userdata);
    private delegate void FilterEventsNativeDelegate(EventFilter filter, IntPtr userdata);
    private static FilterEventsNativeDelegate FilterEventsNativeFunction = SDL_FilterEvents;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_FilterEvents(SDL_EventFilter filter, void *userdata);</code>
    /// <summary>
    /// <para>Run a specific filter function on the current event queue, removing any
    /// events for which the filter returns <c>false</c>.</para>
    /// <para>See <see cref="SetEventFilter"/> for more information. Unlike <see cref="SetEventFilter"/>,
    /// this function does not change the filter permanently, it only uses the
    /// supplied filter until this function returns.</para>
    /// </summary>
    /// <param name="filter">the <see cref="EventFilter"/> function to call when an event happens.</param>
    /// <param name="userdata">a pointer that is passed to <c>filter</c>.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetEventFilter"/>
    /// <seealso cref="SetEventFilter"/>
    public static void FilterEvents(EventFilter filter, IntPtr userdata)
    {
        FilterEventsNativeFunction(filter, userdata);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetEventEnabled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetEventEnabled(uint type, [MarshalAs(UnmanagedType.I1)] bool enabled);
    private delegate void SetEventEnabledNativeDelegate(uint type, bool enabled);
    private static SetEventEnabledNativeDelegate SetEventEnabledNativeFunction = SDL_SetEventEnabled;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetEventEnabled(Uint32 type, bool enabled);</code>
    /// <summary>
    /// Set the state of processing events by type.
    /// </summary>
    /// <param name="type">the type of event; see <see cref="EventType"/> for details.</param>
    /// <param name="enabled">whether to process the event or not.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="EventEnabled"/>
    public static void SetEventEnabled(uint type, bool enabled)
    {
        SetEventEnabledNativeFunction(type, enabled);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_EventEnabled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_EventEnabled(uint type);
    private delegate bool EventEnabledNativeDelegate(uint type);
    private static EventEnabledNativeDelegate EventEnabledNativeFunction = SDL_EventEnabled;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_EventEnabled(Uint32 type);</code>
    /// <summary>
    /// Query the state of processing events by type.
    /// </summary>
    /// <param name="type">the type of event; see <see cref="EventType"/> for details.</param>
    /// <returns><c>true</c> if the event is being processed, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetEventEnabled"/>
    public static bool EventEnabled(uint type)
    {
        return EventEnabledNativeFunction(type);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RegisterEvents"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_RegisterEvents(int numevents);
    private delegate uint RegisterEventsNativeDelegate(int numevents);
    private static RegisterEventsNativeDelegate RegisterEventsNativeFunction = SDL_RegisterEvents;

    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL SDL_RegisterEvents(int numevents);</code>
    /// <summary>
    /// Allocate a set of user-defined events, and return the beginning event
    /// number for that set of events.
    /// </summary>
    /// <param name="numevents">the number of events to be allocated.</param>
    /// <returns>the beginning event number, or 0 if numevents is invalid or if
    /// there are not enough user-defined events left.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PushEvent"/>
    public static uint RegisterEvents(int numevents)
    {
        return RegisterEventsNativeFunction(numevents);
    }


    [ExcludeFromCodeCoverage]
    [DllImport(SDLLibrary, EntryPoint = "SDL_GetWindowFromEvent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static extern IntPtr SDL_GetWindowFromEvent(in Event @event);
    private delegate IntPtr GetWindowFromEventNativeDelegate(in Event @event);
    private static GetWindowFromEventNativeDelegate GetWindowFromEventNativeFunction = SDL_GetWindowFromEvent;

    /// <code>extern SDL_DECLSPEC SDL_Window * SDLCALL SDL_GetWindowFromEvent(const SDL_Event *event);</code>
    /// <summary>
    /// Get window associated with an event.
    /// </summary>
    /// <param name="event">an event containing a <c>windowID</c>.</param>
    /// <returns>the associated window on success or <c>null</c> if there is none.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PollEvent(out Event)"/>
    /// <seealso cref="WaitEvent"/>
    /// <seealso cref="WaitEventTimeout"/>
    public static IntPtr GetWindowFromEvent(in Event @event)
    {
        return GetWindowFromEventNativeFunction(in @event);
    }


    [ExcludeFromCodeCoverage]
    [DllImport(SDLLibrary, EntryPoint = "SDL_GetEventDescription"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static extern int SDL_GetEventDescription(in IntPtr @event, IntPtr buf, int buflen);
    private delegate int GetEventDescriptionNativeDelegate(in IntPtr @event, IntPtr buf, int buflen);
    private static GetEventDescriptionNativeDelegate GetEventDescriptionNativeFunction = SDL_GetEventDescription;
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetEventDescription(const SDL_Event *event, char *buf, int buflen);</code>
    /// <summary>
    /// <para>Generate a human-readable description of an event.</para>
    /// <para>This will fill <c>buf</c> with a <c>null</c>-terminated string that might look
    /// something like this:</para>
    /// <code>EventMouseMotion(timestamp=1140256324 windowid=2 which=0 state=0 x=492.99 y=139.09 xrel=52 yrel=6)</code>
    /// <para>The exact format of the string is not guaranteed; it is intended for
    /// logging purposes, to be read by a human, and not parsed by a computer.</para>
    /// <para>The returned value follows the same rules as SDL_snprintf(): <c>buf</c> will
    /// always be <c>null</c>-terminated (unless <c>buflen</c> is zero), and will be truncated
    /// if <c>buflen</c> is too small. The return code is the number of bytes needed for
    /// the complete string, not counting the <c>null</c>-terminator, whether the string
    /// was truncated or not. Unlike SDL_snprintf(), though, this function never
    /// returns -1.</para>
    /// </summary>
    /// <param name="event">an event to describe. May be <c>null</c>.</param>
    /// <param name="buf">the buffer to fill with the description string. May be <c>null</c>.</param>
    /// <param name="buflen">the maximum bytes that can be written to <c>buf</c>.</param>
    /// <returns>number of bytes needed for the full string, not counting the
    /// <c>null</c>-terminator byte.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    public static int GetEventDescription(in Event @event, byte[]? buf, int buflen)
    {
        var eventPtr = StructureToPointer<Event>(@event);
        try
        {
            var bufPtr = buf is null ? IntPtr.Zero : Marshal.UnsafeAddrOfPinnedArrayElement(buf, 0);
            return GetEventDescriptionNativeFunction(in eventPtr, bufPtr, buflen);
        }
        finally
        {
            Marshal.FreeHGlobal(eventPtr);
        }
    }

    /// <inheritdoc cref="GetEventDescription(in Event, byte[], int)"/>
    public static unsafe int GetEventDescription(in Event @event, Span<byte> buf, int buflen)
    {
        var eventPtr = StructureToPointer<Event>(@event);
        try
        {
            fixed (byte* bufPtr = buf)
            {
                return GetEventDescriptionNativeFunction(in eventPtr, (IntPtr)bufPtr, buflen);
            }
        }
        finally
        {
            Marshal.FreeHGlobal(eventPtr);
        }
    }

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetEventDescription(const SDL_Event *event, char *buf, int buflen);</code>
    /// <summary>
    /// <para>Generate a human-readable description of an event.</para>
    /// <para>This will fill <c>buf</c> with a <c>null</c>-terminated string that might look
    /// something like this:</para>
    /// <code>EventMouseMotion(timestamp=1140256324 windowid=2 which=0 state=0 x=492.99 y=139.09 xrel=52 yrel=6)</code>
    /// <para>The exact format of the string is not guaranteed; it is intended for
    /// logging purposes, to be read by a human, and not parsed by a computer.</para>
    /// <para>The returned value follows the same rules as SDL_snprintf(): <c>buf</c> will
    /// always be <c>null</c>-terminated (unless <c>buflen</c> is zero), and will be truncated
    /// if <c>buflen</c> is too small. The return code is the number of bytes needed for
    /// the complete string, not counting the <c>null</c>-terminator, whether the string
    /// was truncated or not. Unlike SDL_snprintf(), though, this function never
    /// returns -1.</para>
    /// </summary>
    /// <param name="event">an event to describe. May be <c>null</c>.</param>
    /// <param name="buf">the buffer to fill with the description string. May be <c>null</c>.</param>
    /// <param name="buflen">the maximum bytes that can be written to <c>buf</c>.</param>
    /// <returns>number of bytes needed for the full string, not counting the
    /// <c>null</c>-terminator byte.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    public static int GetEventDescription(in IntPtr @event, byte[]? buf, int buflen)
    {
        var bufPtr = buf is null ? IntPtr.Zero : Marshal.UnsafeAddrOfPinnedArrayElement(buf, 0);
        return GetEventDescriptionNativeFunction(in @event, bufPtr, buflen);
    }

    /// <inheritdoc cref="GetEventDescription(in IntPtr, byte[], int)"/>
    public static unsafe int GetEventDescription(in IntPtr @event, Span<byte> buf, int buflen)
    {
        fixed (byte* bufPtr = buf)
        {
            return GetEventDescriptionNativeFunction(in @event, (IntPtr)bufPtr, buflen);
        }
    }

}
