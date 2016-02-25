using System.Collections.Generic;

namespace Model {
    public class Column {
        private int id = 0;
        private string name = "";
        private List<Site> sites = new List<Site>();

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

        public List<Site> Sites {
            get {
                return sites;
            }

            set {
                sites = value;
            }
        }
    }
}
