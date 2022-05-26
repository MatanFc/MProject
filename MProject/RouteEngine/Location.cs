namespace MProject.RouteEngine
{
    public class Location
    {

        string _identifier;
        public Location()
        {

        }
        public string Identifier
        {
            get { return this._identifier; }
            set { this._identifier=value; }
        }
        public override string ToString()
        {
            return _identifier;
        }
    }
}
