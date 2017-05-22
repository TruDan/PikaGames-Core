﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using PikaGames.Games.Core;
using PikaGames.Games.Core.Util;
using PikaGames.Games.Core.Utils;

namespace PikaGames.PaperCast.World
{
    public class Level
    {
        public GameBase Game { get; }

        public int Width => TileWidth * Tile.Size;
        public int Height => TileHeight * Tile.Size;

        public int TileWidth { get; }
        public int TileHeight { get; }

        List<Tile> _grid = new List<Tile>();

        public Level(GameBase game, int tileWidth, int tileHeight)
        {
            Game = game;

            TileWidth = tileWidth;
            TileHeight = tileHeight;

            Initialise();
        }

        private void Initialise()
        {
            for (int i = 0; i < TileWidth; i++)
            {
                for (int j = 0; j < TileHeight; j++)
                {
                    var t = new Tile(Game, i, j);
                    _grid.Add(t);
                }
            }
        }

        public void SpawnPlayer(PaperCastPlayer player)
        {
            var x = 12;
            var y = 12;

            if (player.Input.PlayerIndex == PlayerIndex.Two || player.Input.PlayerIndex == PlayerIndex.Four)
            {
                x = TileWidth - x;
            }

            if (player.Input.PlayerIndex == PlayerIndex.Three || player.Input.PlayerIndex == PlayerIndex.Four)
            {
                y = TileHeight - y;
            }

            player.X = x * Tile.Size;
            player.Y = y * Tile.Size;


            var tile = GetTile(x,y);
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    var t = GetTile(tile.X + i, tile.Y + j);
                    if (t != null)
                    {
                        t.Owner = player;
                        t.UpdateTexture();
                    }
                }
            }
        }

        public Tile GetTile(int x, int y)
        {
            var i = x * TileWidth + y;
            if (i < 0 || i >= _grid.Count)
                return null;
            return _grid[i];
        }

        public Tile GetTileFromPosition(double x, double y)
        {
            var i = (int) (Math.Floor(x / Tile.Size) * TileWidth + Math.Floor(y / Tile.Size));
            if (i >= _grid.Count || i < 0)
            {
                return null;
            }

            return _grid[i];
        }

        public void UpdateClaims(PaperCastPlayer player)
        {
            var sizeX = TileWidth + 2;
            var sizeY = TileHeight + 2;

            var grid = new byte[sizeX,sizeY];


            for (var x = 0; x < sizeX; x++)
            {
                for (var y = 0; y < sizeY; y++)
                {
                    grid[x, y] = 0;

                    if (x == sizeX - 1 || y == sizeY - 1 || x == 0 || y == 0)
                    {
                        grid[x, y] = 1;
                    }
                    else
                    {
                        var tile = GetTile(x-1, y-1);
                        if (tile.Owner == null || tile.Owner != player)
                        {
                            grid[x, y] = 1;
                        }
                    }
                }
            }

            FloodFill.FillGrid(grid, Point.Zero, 2);

            for (var x = 1; x < sizeX-1; x++)
            {
                for (var y = 1; y < sizeY-1; y++)
                {
                    if (grid[x, y] != 2)
                    {
                        var tile = GetTile(x - 1, y - 1);
                        
                        tile.Owner = player;
                        tile.UpdateTexture();
                    }
                }
            }

            UpdatePlayerScore(player);
        }

        public void ClaimTrail(PaperCastPlayer player)
        {
            foreach (var tile in _grid)
            {
                if (tile.PendingOwner == player)
                {
                    tile.Owner = player;
                    tile.PendingOwner = null;
                    tile.UpdateTexture();
                }
            }

            UpdatePlayerScore(player);
        }

        public void UpdatePlayerScore(PaperCastPlayer player)
        {
            player.Score = _grid.Count(t => t.Owner == player);

            if (player.Score == 0)
            {
                KillPlayer(player);
            }
        }

        public void KillPlayer(PaperCastPlayer player)
        {
            player.Kill();

            foreach (var tile in _grid.Where(t => t.Owner == player || t.PendingOwner == player))
            {
                if (tile.Owner == player)
                {
                    tile.Owner = null;
                    tile.UpdateTexture();
                }
                
                if(tile.PendingOwner == player)
                {
                    tile.PendingOwner = null;
                    tile.UpdateTexture();
                }
            }
        }

        internal void Update(GameTime gameTime)
        {

        }

        internal void Draw(GameTime gameTime, Camera2D camera, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);

            foreach (var tile in _grid)
            {
                tile.Draw(spriteBatch);
            }

            spriteBatch.End();

            spriteBatch.Begin(transformMatrix: camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);

            foreach (var player in Game.Players)
            {
                player.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }

    }
}
