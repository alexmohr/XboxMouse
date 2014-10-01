
namespace Controller
{
    /// <summary>
    /// Used to provide functions for classes which listen to gamepad events with polling.
    /// </summary>
    public interface IStoppableListener
    {
        /// <summary>
        /// Starts the listener.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the listener.
        /// </summary>
        void Stop();
    }
}
