﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SpaceWars
{
    /// <inheritdoc />
    /// <summary>
    /// The panel used for drawing the game world.
    /// </summary>
    /// <authors>Jiahui Chen, Mitch Talmadge</authors>
    public sealed class WorldPanel : Panel
    {
        /// <summary>
        /// The game components to be drawn when this component is painted.
        /// </summary>
        private IEnumerable<GameComponent> _gameComponents = new GameComponent[0];

        public WorldPanel()
        {
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Draw background
            using (var brush = new SolidBrush(BackColor))
                e.Graphics.FillRectangle(brush, ClientRectangle);

            // Draw double border
            e.Graphics.DrawRectangle(Pens.White, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1); // Outer
            e.Graphics.DrawRectangle(Pens.Aqua, 2, 2, ClientSize.Width - 5, ClientSize.Height - 5); // Inner

            // Draw game components
            foreach (var gameComponent in _gameComponents)
            {
                var imageDetails = gameComponent.GetDrawingDetails();
                e.Graphics.DrawImage(imageDetails.Item1,
                    new Rectangle(0, 0, imageDetails.Item2.Width, imageDetails.Item2.Height), imageDetails.Item2,
                    GraphicsUnit.Pixel);
            }
        }

        /// <summary>
        /// Schedules the given game components to be drawn on the next tick.
        /// </summary>
        /// <param name="gameComponents">The components to draw, in the order to be drawn.</param>
        public void DrawGameComponents(IEnumerable<GameComponent> gameComponents)
        {
            _gameComponents = gameComponents.ToArray();
        }
    }
}