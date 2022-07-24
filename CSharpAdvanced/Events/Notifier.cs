namespace Events {
    class Notifier {
        // match the signature of event handler delegate
        public void NotifyEnd(object src, EventArgs args) {
            Console.WriteLine("Notify: END");
        }
        
        public void NotifyStart(object src, VideoEventArgs args) {
            Console.WriteLine("Notify: START | ETA: {0}ms", args.Eta);
        }
    }
}