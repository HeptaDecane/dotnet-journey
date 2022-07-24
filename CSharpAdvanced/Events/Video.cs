namespace Events {
    class Video {
        public string Title { get; set; }
        
        public void Encode() {
            Thread.Sleep(2000);     // queue time
            OnVideoEncoding(5000);
            
            Console.WriteLine("Encoding Video... {0}",Title);
            Thread.Sleep(5000);     // encoding time
            
            OnVideoEncoded();
        }

        // 1. define a delegate
        // 2. define an event based on the delegate
        // 3. raise the event
        public delegate void VideoEncodedEventHandler(object src, EventArgs args);
        public event VideoEncodedEventHandler VideoEncoded;
        protected virtual void OnVideoEncoded() {
            if (VideoEncoded != null) 
                VideoEncoded(this, EventArgs.Empty);
        }
        
        
        // another approach (built in Event Handler)
        public event EventHandler<VideoEventArgs> VideoEncoding;
        protected virtual void OnVideoEncoding(int eta) {
            if (VideoEncoding != null) 
                VideoEncoding(this, new VideoEventArgs() { Eta = eta });
        }
    }

    // custom event args
    class VideoEventArgs : EventArgs {
        public int Eta { get; set; }
    }
}