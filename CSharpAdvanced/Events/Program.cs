namespace Events {
    class Program {
        static void Main(string[] args) {
            var video = new Video() { Title = "Video001.mp4" };     // publisher
            var notifier = new Notifier();    // subscriber

            // subscribing to the event `VideoEncoded`
            video.VideoEncoding += notifier.NotifyStart;
            video.VideoEncoded += notifier.NotifyEnd;
            video.Encode();
        }
    }
}

