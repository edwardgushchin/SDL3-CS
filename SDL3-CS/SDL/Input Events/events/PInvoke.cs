#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024 Eduard Gushchin.
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
 *
 * Eduard "edwardgushchin" Gushchin <eduardgushchin@yandex.ru>
 *
 */
#endregion

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PumpEvents();
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_PumpEvents(void);</code>
    /// <summary>
    /// <para>Pump the event loop, gathering events from the input devices.</para>
    /// <para>This function updates the event queue and internal input device state.</para>
    /// <para><b>WARNING</b>: This should only be run in the thread that initialized the
    /// video subsystem, and for extra safety, you should consider only doing those
    /// things on the main thread in any case.</para>
    /// <para><see cref="PumpEvents"/> gathers all the pending input information from devices and
    /// places it in the event queue. Without calls to <see cref="PumpEvents"/> no events
    /// would ever be placed on the queue. Often the need for calls to
    /// <see cref="PumpEvents"/> is hidden from the user since <see cref="PollEvent"/> and
    /// <see cref="WaitEvent"/> implicitly call <see cref="PumpEvents"/>. However, if you are not
    /// polling or waiting for events (e.g. you are filtering them), then you must
    /// call <see cref="PumpEvents"/> to force an event queue update.</para>
    /// </summary>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="PollEvent"/>
    /// <seealso cref="WaitEvent"/>
    public static void PumpEvents() => SDL_PumpEvents();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_PeepEvents(IntPtr events, int numevents, EventAction action, uint minType,
        uint maxType);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_PeepEvents(SDL_Event *events, int numevents, SDL_EventAction action, Uint32 minType, Uint32 maxType);</code>
    /// <summary>
    /// <para>Check the event queue for messages and optionally return them.</para>
    /// <para><c>action</c> may be any of the following:</para>
    /// <list type="bullet">
    /// <item><see cref="EventAction.AddEvent"/>: up to <c>numevents</c> events will be added to the back of the
    /// event queue.</item>
    /// <item><see cref="EventAction.PeekEvent"/>: <c>numevents</c> events at the front of the event queue,
    /// within the specified minimum and maximum type, will be returned to the
    /// caller and will <i>not</i> be removed from the queue.</item>
    /// <item><see cref="EventAction.GetEvent"/>: up to <c>numevents</c> events at the front of the event queue,
    /// within the specified minimum and maximum type, will be returned to the
    /// caller and will be removed from the queue.</item>
    /// </list>
    /// <para>You may have to call <see cref="SDL.PumpEvents"/> before calling this function.
    /// Otherwise, the events may not be ready to be filtered when you call
    /// <see cref="SDL.PeepEvents"/>.</para>
    /// <para>This function is thread-safe.</para>
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
    /// <returns>the number of events actually stored or a negative error code on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="PollEvent"/>
    /// <seealso cref="PumpEvents"/>
    /// <seealso cref="PushEvent"/>
    public static int PeepEvents(Event[] events, int numevents, EventAction action, uint minType, uint maxType)
    {
        var eventsPtr = events != null ? Marshal.UnsafeAddrOfPinnedArrayElement(events, 0) : IntPtr.Zero;
        return SDL_PeepEvents(eventsPtr, numevents, action, minType, maxType);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasEvent(uint type);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasEvent(Uint32 type);</code>
    /// <summary>
    /// <para>Check for the existence of a certain event type in the event queue.</para>
    /// <para>If you need to check for a range of event types, use <see cref="HasEvents"/> instead.</para>
    /// </summary>
    /// <param name="type">The type of event to be queried; see <see cref="EventType"/> for details.</param>
    /// <returns><c>true</c> if events matching <c>type</c> are present, or <c>false</c> if
    /// events matching <c>type</c> are not present.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HasEvents"/>
    public static bool HasEvent(uint type) => SDL_HasEvent(type);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasEvents(uint minType, uint maxType);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasEvents(Uint32 minType, Uint32 maxType);</code>
    /// <summary>
    /// <para>Check for the existence of certain event types in the event queue.</para>
    /// <para>If you need to check for a single event type, use <see cref="HasEvent"/> instead.</para>
    /// </summary>
    /// <param name="minType">The low end of the event type range to be queried, inclusive; see
    /// <see cref="EventType"/> for details.</param>
    /// <param name="maxType">The high end of the event type range to be queried, inclusive; see
    /// <see cref="EventType"/> for details.</param>
    /// <returns><c>true</c> if events with type >= <c>minType</c> and &lt;= <c>maxType</c> are
    /// present, or <c>false</c> if not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HasEvent"/>
    public static bool HasEvents(uint minType, uint maxType) => SDL_HasEvents(minType, maxType);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_FlushEvent(uint type);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_FlushEvent(Uint32 type);</code>
    /// <summary>
    /// <para>Clear events of a specific type from the event queue.</para>
    /// <para>This will unconditionally remove any events from the queue that match
    /// <c>type</c>. If you need to remove a range of event types, use <see cref="FlushEvents"/> instead.</para>
    /// <para>It's also normal to just ignore events you don't care about in your event
    /// loop without calling this function.</para>
    /// <para>This function only affects currently queued events. If you want to make
    /// sure that all pending OS events are flushed, you can call <see cref="PumpEvents"/>
    /// on the main thread immediately before the flush call.</para>
    /// <para>If you have user events with custom data that needs to be freed, you should
    /// use <see cref="PeepEvents"/> to remove and clean up those events before calling
    /// this function.</para>
    /// </summary>
    /// <param name="type">The type of event to be cleared; see <see cref="EventType"/> for details.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="FlushEvents"/>
    public static void FlushEvent(uint type) => SDL_FlushEvent(type);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_FlushEvents(uint minType, uint maxType);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_FlushEvents(Uint32 minType, Uint32 maxType);</code>
    /// <summary>
    /// <para>Clear events of a range of types from the event queue.</para>
    /// <para>This will unconditionally remove any events from the queue that are in the
    /// range of <paramref name="minType"/> to <paramref name="maxType"/>, inclusive. If you need to remove a single
    /// event type, use <see cref="FlushEvent"/> instead.</para>
    /// <para>It's also normal to just ignore events you don't care about in your event
    /// loop without calling this function.</para>
    /// <para>This function only affects currently queued events. If you want to make
    /// sure that all pending OS events are flushed, you can call <see cref="PumpEvents"/>
    /// on the main thread immediately before the flush call.</para>
    /// </summary>
    /// <param name="minType">The low end of event type to be cleared, inclusive; see
    /// <see cref="EventType"/> for details.</param>
    /// <param name="maxType">The high end of event type to be cleared, inclusive; see
    /// <see cref="EventType"/> for details.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="FlushEvent"/>
    public static void FlushEvents(uint minType, uint maxType) => SDL_FlushEvents(minType, maxType);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_PollEvent(out Event e);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_PollEvent(SDL_Event *event);</code>
    /// <summary>
    /// Poll for currently pending events.
    /// </summary>
    /// <para>If <c>e</c> is not <c>null</c>, the next event is removed from the queue and stored
    /// in the <see cref="Event"/> structure pointed to by <c>e</c> . The 1 returned refers to
    /// this event, immediately stored in the <see cref="Event"/> structure—not an event
    /// to follow.</para>
    /// <para>If <c>e</c>  is <c>null</c>, it simply returns 1 if there is an event in the queue,
    /// but will not remove it from the queue.</para>
    /// <para>As this function may implicitly call <see cref="PumpEvents"/>, you can only call
    /// this function in the thread that set the video mode.</para>
    /// <para><see cref="PollEvent"/> is the favored way of receiving system events since it can
    /// be done from the main loop and does not suspend the main loop while waiting
    /// on an event to be posted.</para>
    /// <para>The common practice is to fully process the event queue once every frame,
    /// usually as a first step before updating the game's state:</para>
    /// <param name="e">The <see cref="Event"/> structure to be filled with the next event from
    /// the queue, or <c>null</c>.</param>
    /// <returns><c>true</c> if this got an event or <c>false</c> if there are none
    /// available.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="PushEvent"/>
    /// <seealso cref="WaitEvent"/>
    /// <seealso cref="WaitEventTimeout"/>
    public static bool PollEvent(out Event e) => SDL_PollEvent(out e);


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_WaitEvent(out Event e);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_WaitEvent(SDL_Event *event);</code>
    /// <summary>
    /// Wait indefinitely for the next available event.
    /// </summary>
    /// <para>If <c>e</c> is not <c>null</c>, the next event is removed from the queue and stored
    /// in the <see cref="Event"/> structure pointed to by <c>e</c>.</para>
    /// <para>As this function may implicitly call <see cref="PumpEvents"/>, you can only call
    /// this function in the thread that initialized the video subsystem.</para>
    /// <param name="e">The <see cref="Event"/> structure to be filled in with the next event
    /// from the queue, or <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> if there was an error while
    /// waiting for events; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="PollEvent"/>
    /// <seealso cref="PushEvent"/>
    /// <seealso cref="WaitEventTimeout"/>
    public static bool WaitEvent(out Event e) => SDL_WaitEvent(out e);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WaitEventTimeout(out Event e, int timeoutMs);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_WaitEventTimeout(SDL_Event *event, Sint32 timeoutMS);</code>
    /// <summary>
    /// Wait until the specified timeout (in milliseconds) for the next available
    /// event.
    /// </summary>
    /// <para>If <c>e</c> is not <c>null</c>, the next event is removed from the queue and stored
    /// in the <see cref="Event"/> structure pointed to by <c>e</c>.</para>
    /// <para>As this function may implicitly call <see cref="PumpEvents"/>, you can only call
    /// this function in the thread that initialized the video subsystem.</para>
    /// <para>The timeout is not guaranteed; the actual wait time could be longer due to
    /// system scheduling.</para>
    /// <param name="e">The <see cref="Event"/> structure to be filled in with the next event
    /// from the queue, or <c>null</c>.</param>
    /// <param name="timeoutMs">The maximum number of milliseconds to wait for the next
    /// available event.</param>
    /// <returns><c>true</c> if this got an event or <c>false</c> if the timeout elapsed
    /// without any events available.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="PollEvent"/>
    /// <seealso cref="PushEvent"/>
    /// <seealso cref="WaitEvent"/>
    public static bool WaitEventTimeout(out Event e, int timeoutMs) => SDL_WaitEventTimeout(out e, timeoutMs);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_PushEvent(ref Event e);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_PushEvent(SDL_Event *event);</code>
    /// <summary>
    /// Add an event to the event queue.
    /// </summary>
    /// <para>The event queue can actually be used as a two-way communication channel.
    /// Not only can events be read from the queue, but the user can also push
    /// their own events onto it. <c>e</c> is a pointer to the event structure you
    /// wish to push onto the queue. The event is copied into the queue, and the
    /// caller may dispose of the memory pointed to after <see cref="PushEvent"/> returns.</para>
    /// <para><b>Note:</b> Pushing device input events onto the queue doesn't modify the state
    /// of the device within SDL.</para>
    /// <para>This function is thread-safe, and can be called from other threads safely.</para>
    /// <para><b>Note:</b> Events pushed onto the queue with <see cref="PushEvent"/> get passed through
    /// the event filter but events added with <see cref="PeepEvents"/> do not.</para>
    /// <para>For pushing application-specific events, please use <see cref="RegisterEvents"/> to
    /// get an event type that does not conflict with other code that also wants
    /// its own custom event types.</para>
    /// <param name="e">The SDL_Event to be added to the queue.</param>
    /// <returns><c>1</c> on success, <c>0</c> if the event was filtered, or a negative error
    /// code on failure; call <see cref="GetError"/> for more information. A
    /// common reason for error is the event queue being full.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="PeepEvents"/>
    /// <seealso cref="PollEvent"/>
    /// <seealso cref="RegisterEvents"/>
    public static int PushEvent(ref Event e) => SDL_PushEvent(ref e);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetEventFilter(EventFilter filter, IntPtr userdata);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetEventFilter(SDL_EventFilter filter, void *userdata);</code>
    /// <summary>
    /// Set up a filter to process all events before they change internal state and
    /// are posted to the internal event queue.
    /// </summary>
    /// <para>If the filter function returns <c>1</c> when called, then the event will be added
    /// to the internal queue. If it returns <c>0</c>, then the event will be dropped from
    /// the queue, but the internal state will still be updated. This allows
    /// selective filtering of dynamically arriving events.</para>
    /// <para><b>WARNING</b>: Be very careful of what you do in the event filter function,
    /// as it may run in a different thread!</para>
    /// <para>On platforms that support it, if the quit event is generated by an
    /// interrupt signal (e.g. pressing Ctrl-C), it will be delivered to the
    /// application at the next event poll.</para>
    /// <para>There is one caveat when dealing with the <see cref="EventType.Quit"/> event type. The
    /// event filter is only called when the window manager desires to close the
    /// application window. If the event filter returns <c>1</c>, then the window will be
    /// closed, otherwise the window will remain open if possible.</para>
    /// <para>Note: Disabled events never make it to the event filter function; see
    /// <see cref="SDL.SetEventEnabled"/>.</para>
    /// <para>Note: If you just want to inspect events without filtering, you should use
    /// <see cref="SDL.AddEventWatch"/> instead.</para>
    /// <para>Note: Events pushed onto the queue with <see cref="SDL.PushEvent"/> get passed through
    /// the event filter, but events pushed onto the queue with <see cref="SDL.PeepEvents"/> do
    /// not.</para>
    /// <param name="filter">An <see cref="SDL.EventFilter"/> function to call when an event happens.</param>
    /// <param name="userdata">A pointer that is passed to <c>filter</c>.</param>
    /// <threadsafety>SDL may call the filter callback at any time from any thread;
    /// the application is responsible for locking resources the
    /// callback touches that need to be protected.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SDL.AddEventWatch"/>
    /// <seealso cref="SDL.SetEventEnabled"/>
    /// <seealso cref="SDL.GetEventFilter"/>
    /// <seealso cref="SDL.PeepEvents"/>
    /// <seealso cref="SDL.PushEvent"/>
    public static void SetEventFilter(EventFilter filter, IntPtr userdata) => SDL_SetEventFilter(filter, userdata);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GetEventFilter(out EventFilter filter, out IntPtr userdata);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_GetEventFilter(SDL_EventFilter *filter, void **userdata);</code>
    /// <summary>
    /// Query the current event filter.
    /// </summary>
    /// <para>This function can be used to "chain" filters, by saving the existing filter
    /// before replacing it with a function that will call that saved filter.</para>
    /// <param name="filter">The current callback function will be stored here.</param>
    /// <param name="userdata">The pointer that is passed to the current event filter will
    /// be stored here.</param>
    /// <returns><c>true</c> on success or <c>false</c> if there is no event filter set.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SDL.SetEventFilter"/>
    public static bool GetEventFilter(out EventFilter filter, out IntPtr userdata) =>
        SDL_GetEventFilter(out filter, out userdata);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddEventWatch(EventFilter filter, IntPtr userdata);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_AddEventWatch(SDL_EventFilter filter, void *userdata);</code>
    /// <summary>
    /// Add a callback to be triggered when an event is added to the event queue.
    /// </summary>
    /// <para><c>filter</c> will be called when an event happens, and its return value is
    /// ignored.</para>
    /// <para><b>WARNING:</b> Be very careful of what you do in the event filter function,
    /// as it may run in a different thread!</para>
    /// <para>If the quit event is generated by a signal (e.g., SIGINT), it will bypass
    /// the internal queue and be delivered to the watch callback immediately, and
    /// arrive at the next event poll.</para>
    /// <para>Note: The callback is called for events posted by the user through
    /// <see cref="SDL.PushEvent"/> but not for disabled events, nor for events by a filter
    /// callback set with <see cref="SDL.SetEventFilter"/>, nor for events posted by the user
    /// through <see cref="SDL.PeepEvents"/>.</para>
    /// <param name="filter">An <c>SDL_EventFilter</c> function to call when an event happens.</param>
    /// <param name="userdata">A pointer that is passed to <c>filter</c>.</param>
    /// <returns>0 on success, or a negative error code on failure; call <see cref="SDL.GetError"/> 
    /// for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SDL.DelEventWatch"/>
    /// <seealso cref="SDL.SetEventFilter"/>
    public static int AddEventWatch(EventFilter filter, IntPtr userdata) => SDL_AddEventWatch(filter, userdata);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DelEventWatch(EventFilter filter, IntPtr userdata);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DelEventWatch(SDL_EventFilter filter, void *userdata);</code>
    /// <summary>
    /// Remove an event watch callback added with <see cref="SDL.AddEventWatch"/>.
    /// </summary>
    /// <para>This function takes the same input as <see cref="SDL.AddEventWatch"/> to identify and
    /// delete the corresponding callback.</para>
    /// <param name="filter">The function originally passed to <see cref="SDL.AddEventWatch"/>.</param>
    /// <param name="userdata">The pointer originally passed to <see cref="SDL.AddEventWatch"/>.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SDL.AddEventWatch"/>
    public static void DelEventWatch(EventFilter filter, IntPtr userdata) => SDL_DelEventWatch(filter, userdata);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_FilterEvents(EventFilter filter, IntPtr userdata);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_FilterEvents(SDL_EventFilter filter, void *userdata);</code>
    /// <summary>
    /// Run a specific filter function on the current event queue, removing any
    /// events for which the filter returns 0.
    /// </summary>
    /// <para>This function uses the provided filter function to process events in the
    /// queue, removing those for which the filter returns 0. Unlike <see cref="SDL.SetEventFilter"/>,
    /// this function does not permanently set the filter; it only applies the filter
    /// during the execution of this function.</para>
    /// <param name="filter">The <see cref="EventFilter"/> function to call when an event happens.</param>
    /// <param name="userdata">A pointer that is passed to the <c>filter</c> function.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SDL.GetEventFilter"/>
    /// <seealso cref="SDL.SetEventFilter"/>
    public static void FilterEvents(EventFilter filter, IntPtr userdata) => SDL_FilterEvents(filter, userdata);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetEventEnabled(uint type, [MarshalAs(SDLBool)] bool enabled);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetEventEnabled(Uint32 type, SDL_bool enabled);</code>
    /// <summary>
    /// Set the state of processing events by type.
    /// </summary>
    /// <param name="type">The type of event; see <see cref="EventType"/> for details.</param>
    /// <param name="enabled">Whether to process the event or not. Use <c>true</c> to enable processing or <c>false</c> to disable.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="EventEnabled"/>
    public static void SetEventEnabled(uint type, bool enabled) => SDL_SetEventEnabled(type, enabled);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_EventEnabled(uint type);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_EventEnabled(Uint32 type);</code>
    /// <summary>
    /// Query the state of processing events by type.
    /// </summary>
    /// <param name="type">The type of event; see <see cref="EventType"/> for details.</param>
    /// <returns><c>true</c> if the event is being processed, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SDL.SetEventEnabled"/>
    public static bool EventEnabled(uint type) => SDL_EventEnabled(type);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_RegisterEvents(int numevents);
    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL SDL_RegisterEvents(int numevents);</code>
    /// <summary>
    /// Allocate a set of user-defined events and return the beginning event number for that set of events.
    /// </summary>
    /// <param name="numevents">The number of events to be allocated.</param>
    /// <returns>The beginning event number for the allocated set, or 0 if <paramref name="numevents"/> is
    /// invalid or if there are not enough user-defined events left.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SDL.PushEvent"/>
    public static uint RegisterEvents(int numevents) => SDL_RegisterEvents(numevents);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_AllocateEventMemory(UIntPtr size);
    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_AllocateEventMemory(size_t size);</code>
    /// <summary>
    /// Allocate temporary memory for an SDL event.
    /// </summary>
    /// <param name="size">The amount of memory to allocate.</param>
    /// <returns>A pointer to the allocated memory, or <c>null</c> on failure; call
    /// <see cref="SDL_GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SDL_FreeEventMemory"/>
    public static IntPtr AllocateEventMemory(UIntPtr size) => SDL_AllocateEventMemory(size);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_FreeEventMemory();
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_FreeEventMemory(void);</code>
    /// <summary>
    /// Free temporary event memory allocated by SDL.
    /// </summary>
    /// <remarks>
    /// This function frees temporary memory allocated for events and APIs that
    /// return temporary strings. This memory is local to the thread that creates
    /// it and is automatically freed for the main thread when pumping the event
    /// loop. For other threads, you may want to call this function periodically to
    /// free any temporary memory created by that thread.
    /// Note that if you call <see cref="AllocateEventMemory"/> on one thread and
    /// pass it to another thread (e.g., via a user event), then you should be sure
    /// the other thread has finished processing it before calling this function.
    /// </remarks>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="AllocateEventMemory"/>
    public static void FreeEventMemory() => SDL_FreeEventMemory();
}