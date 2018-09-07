// Daniel Morato Baudi.
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace XNA.Tiled.CollisionParser
{
    public class Collider
    {
        #region Attributes
        private string name;
        private string type;
        private Vector2 position;
        private List<Vector2> points;
        private bool isTrigger;
        private Vector2 dimensions;
        private BoundingBox box;
        private Rectangle bounds;
        #endregion

        #region Constructors
        /// <summary>
        /// Initiallizes a new empty Collider.
        /// </summary>
        public Collider()
        {
            this.name = "";
            this.type = "";
            this.position = new Vector2(0, 0);
            this.points = new List<Vector2>();
            this.isTrigger = false;
            this.dimensions = new Vector2(0, 0);
            
            this.CreateBox();
        }

        /// <summary>
        /// Initiallizes a new lineal Collider.
        /// </summary>
        /// <param name="name">Collider's name.</param>
        /// <param name="type">Collider's type.</param>
        /// <param name="position">Collider's inital position.</param>
        /// <param name="points">Collider's separation points.</param>
        /// <param name="isTrigger">Checks if the collider is a trigger.</param>
        public Collider(string name, string type, Vector2 position,
                        List<Vector2> points, bool isTrigger)
        {
            this.name = name;
            this.type = type;
            this.position = position;

            this.points = new List<Vector2>();
            foreach (Vector2 point in points)
                this.points.Add(point);

            this.isTrigger = isTrigger;
            this.CreateBox();
        }

        /// <summary>
        /// Initiallizes a new square Collider.
        /// </summary>
        /// <param name="name">Collider's name.</param>
        /// <param name="type">Collider's type.</param>
        /// <param name="position">Collider's inital position.</param>
        /// <param name="isTrigger">Checks if the collider is a trigger.</param>
        /// <param name="dimensions">Collider's dimensions.</param>
        public Collider(string name, string type, Vector2 position,
                        bool isTrigger, Vector2 dimensions)
        {
            this.name = name;
            this.type = type;
            this.position = position;
            this.isTrigger = isTrigger;
            this.dimensions = dimensions;

            this.CreateBox();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a box used to check collisions.
        /// </summary>
        public void CreateBox()
        {
            this.box = new BoundingBox();
            this.bounds = new Rectangle();

            if (this.Type == "BoxCollider")
            {
                this.box = new BoundingBox(
                    new Vector3(this.Position, 0),
                    new Vector3(this.Dimensions.X + this.Position.X,
                        this.Dimensions.Y + this.Position.Y, 0));

                this.bounds = new Rectangle(
                    (int)this.Position.X,
                    (int)this.Position.Y,
                    (int)this.Dimensions.X,
                    (int)this.Dimensions.Y);

            } else if (this.Type == "LineCollider")
                for (int i = 0; i < this.Points.Count; i++)
                {
                    this.box = new BoundingBox(
                        new Vector3(this.Position.X + this.Points[i].X,
                                    this.Position.Y + this.Points[i].Y, 0),
                        new Vector3(System.Math.Abs((int)this.Points[i].X),
                            System.Math.Abs((int)this.Points[i].Y), 0));

                    this.bounds = new Rectangle(
                        (int)this.Position.X + (int)this.Points[i].X,
                        (int)this.Position.Y + (int)this.Points[i].Y,
                        System.Math.Abs((int)this.Points[i].X) + 2,
                        System.Math.Abs((int)this.Points[i].Y) + 2);
                }
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get { return this.name;  }
            set { this.name = value; }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type
        {
            get { return this.type;  }
            set { this.type = value; }
        }

        /// <summary>
        /// Returns the initial position.
        /// </summary>
        public Vector2 Position
        {
            get { return this.position; }
        }

        /// <summary>
        /// Returns the list of points.
        /// </summary>
        public List<Vector2> Points
        {
            get { return this.points; }
        }

        /// <summary>
        /// Returns the trigger status.
        /// </summary>
        public bool IsTrigger
        {
            get { return this.isTrigger; }
        }

        /// <summary>
        /// Returns the dimensions.
        /// </summary>
        public Vector2 Dimensions
        {
            get { return this.dimensions; }
        }

        /// <summary>
        /// Returns the colliding box.
        /// </summary>
        public BoundingBox Box
        {
            get { return this.box; }
        }

        /// <summary>
        /// Returns the colliding box bounds.
        /// </summary>
        public Rectangle Bounds
        {
            get { return this.bounds; }
        }
        #endregion
    }
}