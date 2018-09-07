// Daniel Morato Baudi
using System.Xml;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace XNA.Tiled.CollisionParser
{
    public class TiledParser
    {
        #region Attributes
        private XmlDocument xml;
        private XmlNodeList colliders;
        private XmlNodeList nodeList;
        private XmlNodeList polylineList;
        private List<Collider> collidersList;
        #endregion

        #region Constructor
        public TiledParser()
        {
            this.xml = new XmlDocument();
            this.collidersList = 
                new List<Collider>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Parses a XML file to extract the colliders.
        /// </summary>
        /// <param name="path">XML file path.</param>
        /// <returns>List of colliders.</returns>
        public List<Collider> Parse(string path)
        {
            try {

                xml.Load(path);
                colliders = xml.
                    GetElementsByTagName(
                    "objectgroup");
            
                nodeList = ((XmlElement)colliders[0])
                    .GetElementsByTagName("object");

            } catch (System.IO.DirectoryNotFoundException ex) {
                System.Diagnostics.Debug
                    .WriteLine("File not found!");

            } catch (System.Exception ex) {
                System.Diagnostics.Debug
                    .WriteLine("Error: " + ex.Message);
            }

            Collider collider;
            string name, type;
            float x, y, width = 0;
            float height = 0;
            bool isTrigger;
            List<Vector2> points;

            if (nodeList != null)
                foreach (XmlElement node in nodeList)
                {
                    collider = new Collider();
                    name = node.GetAttribute("name");
                    type = node.GetAttribute("type");
                    x = System.Convert.ToSingle(node.GetAttribute("x"));
                    y = System.Convert.ToSingle(node.GetAttribute("y"));
                    isTrigger = type == "Trigger" ? true : false;
                    points = new List<Vector2>();

                    if (node.HasAttribute("width"))
                        width = System.Convert.ToSingle(
                            node.GetAttribute("width"));

                    if (node.HasAttribute("height"))
                        height = System.Convert.ToSingle(
                            node.GetAttribute("height"));

                    if (node.HasChildNodes)
                    {
                        polylineList = node.GetElementsByTagName("polyline");
                        foreach (XmlElement polyline in polylineList)
                            points = ParsePolyline(polyline
                                .GetAttribute("points"));
                    }

                    if (type == "BoxCollider")
                        collider = new Collider(name, type,
                            new Vector2(x, y), isTrigger,
                            new Vector2(width, height));
                    else
                        collider = new Collider(name,
                            type, new Vector2(x, y),
                            points, isTrigger);

                    collidersList.Add(collider);
                }

            return collidersList;
        }

        /// <summary>
        /// Parses the polyline's points.
        /// </summary>
        /// <param name="line">Polyline's points.</param>
        /// <returns>Parsed points.</returns>
        public List<Vector2> ParsePolyline(string line)
        {
            string[] aux = line.Split(' ');
            string[] vectorPoints;
            List<Vector2> points = new List<Vector2>();

            for (int i = 0; i < aux.Length; i++)
            {
                vectorPoints = aux[i].Split(',');
                points.Add(new Vector2(
                    System.Convert.ToSingle(vectorPoints[0]),
                    System.Convert.ToSingle(vectorPoints[1])));
            }

            return points;
        }
        #endregion
    }
}