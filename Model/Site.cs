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
                PrependHttpToUrl(url);
                return url;
            }

            set {
                PrependHttpToUrl(value);

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

        ///<summary>
        /// Checks the given url to see if it has http or https and adds it in if its missing
        ///</summary>
        private string PrependHttpToUrl(string urlToCheck) {
            if(!urlToCheck.StartsWith("http://") && !urlToCheck.StartsWith("https://")) {
                urlToCheck = "http://" + urlToCheck;
            }

            return urlToCheck;
        }
    }
}
