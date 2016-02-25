namespace Model {
    public class Site {
        private int id = 0;
        private string name = "";
        private string url = "";
        private string bgColour = "";
        private string colour = "";

        public int Id {
            get {
                return id;
            }

            set {
                id = value;
            }
        }

        public string Name {
            get {
                return name;
            }

            set {
                name = value;
            }
        }

        public string Url {
            get {
                return url;
            }

            set {
                url = value;
            }
        }

        public string BgColour {
            get {
                return bgColour;
            }

            set {
                bgColour = value;
            }
        }

        public string Colour {
            get {
                return colour;
            }

            set {
                colour = value;
            }
        }
    }
}
