using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using PikaGames.Games.Core.Utils;

namespace PikaGames.Games.RaceCast.Util
{
	public class Vehicle : RigidBody
	{
		private class Wheel
		{
			private Vector2 _mForwardAxis, _mSideAxis;
			private float _mWheelTorque, _mWheelSpeed, _mWheelInertia, _mWheelRadius;
			private Vector2 _mPosition = new Vector2();
			private bool _locked = false;

			public Wheel(Vector2 position, float radius)
			{
				_mPosition = position;
				SetSteeringAngle(0);
				_mWheelSpeed = 0;
				_mWheelRadius = radius;
				_mWheelInertia = radius * radius; //fake value
			}

			public void SetSteeringAngle(float newAngle)
			{
				Matrix mat = Matrix.CreateRotationZ(newAngle / (float)Math.PI * 180.0f);

				_mForwardAxis = Vector2.Transform(new Vector2(0, 1), mat);
				_mSideAxis = Vector2.Transform(new Vector2(-1, 0), mat);
			}

			public void AddTransmissionTorque(float newValue)
			{
				_mWheelTorque += newValue;
			}

			public float GetWheelSpeed()
			{
				return _mWheelSpeed;
			}

			public Vector2 GetAttachPoint()
			{
				return _mPosition;
			}

			public Vector2 CalculateForce(Vector2 relativeGroundSpeed, float timeStep, float friction)
			{
				if (_locked)
				{
					_mWheelTorque += -_mWheelSpeed * 100;
				}

				//calculate speed of tire patch at ground
				Vector2 patchSpeed = -_mForwardAxis * _mWheelSpeed * _mWheelRadius;

				//get velocity difference between ground and patch
				Vector2 velDifference = relativeGroundSpeed + patchSpeed;

				//project ground speed onto side axis
				float forwardMag = 0;
				Vector2 sideVel = velDifference.Project(_mSideAxis);
				Vector2 forwardVel = velDifference.Project(_mForwardAxis, out forwardMag);

				//calculate super fake friction forces
				//calculate response force
				Vector2 responseForce = -sideVel * friction;
				responseForce -= forwardVel;

				//calculate torque on wheel
				_mWheelTorque += forwardMag * _mWheelRadius;

				//integrate total torque into wheel
				_mWheelSpeed += _mWheelTorque / _mWheelInertia * timeStep;

				//clear our transmission torque accumulator
				_mWheelTorque = 0;

				//return force acting on body
				return responseForce;
			}

			public void SetLock(bool locked)
			{
				_locked = locked;
			}
		}
		private Wheel[] _wheels = new Wheel[4];

		public override void Setup(Level level, Vector2 halfSize, float mass)
		{
			//front wheels
			_wheels[0] = new Wheel(new Vector2(halfSize.X, halfSize.Y), 0.5f);
			_wheels[1] = new Wheel(new Vector2(-halfSize.X, halfSize.Y), 0.5f);

			//rear wheels
			_wheels[2] = new Wheel(new Vector2(halfSize.X, -halfSize.Y), 0.5f);
			_wheels[3] = new Wheel(new Vector2(-halfSize.X, -halfSize.Y), 0.5f);

			base.Setup(level, halfSize, mass);
		}

		public void SetSteering(float steering)
		{
			const float steeringLock = 0.75f;

			//apply steering angle to front wheels
			_wheels[0].SetSteeringAngle(-steering * steeringLock);
			_wheels[1].SetSteeringAngle(-steering * steeringLock);
		}

		public void SetThrottle(float throttle, bool allWheel)
		{
			const float torque = 20.0f;

			//apply transmission torque to back wheels
			if (allWheel)
			{
				_wheels[2].AddTransmissionTorque(throttle * torque);
				_wheels[3].AddTransmissionTorque(throttle * torque);
			}

			_wheels[0].AddTransmissionTorque(throttle * torque);
			_wheels[1].AddTransmissionTorque(throttle * torque);
		}

		public void SetBrakes(float brakes)
		{
			const float brakeTorque = 4.0f;

			//apply brake torque apposing wheel vel
			foreach (Wheel wheel in _wheels)
			{
				float wheelVel = wheel.GetWheelSpeed();
				wheel.AddTransmissionTorque(-wheelVel * brakeTorque * brakes);
			}
		}

		public void LockWheels(bool locked, bool allWheels)
		{
			_wheels[0].SetLock(locked);
			_wheels[1].SetLock(locked);

			if (allWheels)
			{
				_wheels[2].SetLock(locked);
				_wheels[3].SetLock(locked);
			}
		}

		public override void Update(float timeStep)
		{
			foreach (Wheel wheel in _wheels)
			{
				var tile = Level.GetTile(this.GetPosition() + base.RelativeToWorld(wheel.GetAttachPoint()));
				float friction = 2f;
				if (tile != null)
				{
					friction = tile.Friction;
				//	Debug.WriteLine($"Applied friction: {tile.X}, {tile.Y}");
				}

				//wheel.m_wheelSpeed = 30.0f;
				Vector2 worldWheelOffset = base.RelativeToWorld(wheel.GetAttachPoint());
				Vector2 worldGroundVel = base.PointVel(worldWheelOffset);
				Vector2 relativeGroundSpeed = base.WorldToRelative(worldGroundVel);
				Vector2 relativeResponseForce = wheel.CalculateForce(relativeGroundSpeed, timeStep, friction);
				Vector2 worldResponseForce = base.RelativeToWorld(relativeResponseForce);

				base.AddForce(worldResponseForce, worldWheelOffset);
			}

			base.Update(timeStep);
		}

		protected override bool CanMove(Vector2 pos)
		{
			//return CanMoveHere(pos);
			foreach (var wheel in _wheels)
			{
				var wheelPos = pos + base.RelativeToWorld(wheel.GetAttachPoint());
				if (!CanMoveHere(wheelPos)) return false;
			}

			//var frontRight = Rotate(pos, new Vector2(_mHalfSize.X, _mHalfSize.Y), GetAngle());
			//if (!CanMoveHere(frontRight)) return false;

			return true;
		}

		private bool CanMoveHere(Vector2 pos)
		{
			if (pos.X < 0 || pos.X > Level.PixelWidth) return false;
			if (pos.Y < 0 || pos.Y > Level.PixelHeight) return false;

			var tile = Level.GetTile(pos);
			if (tile == null) return false;

			return !tile.IsSolid;
		}

		private Vector2 Rotate(Vector2 center, Vector2 corner, float theta)
		{
			float tempX = corner.X - center.X;
			float tempY = corner.Y - center.Y;

			// now apply rotation
			float rotatedX = (float) (tempX * Math.Cos(theta) - tempY * Math.Sin(theta));
			float rotatedY = (float) (tempX * Math.Sin(theta) + tempY * Math.Cos(theta));

			// translate back
			var x = rotatedX + center.X;
			var y = rotatedY + center.Y;

			return new Vector2(x,y);
		}
	}

	public class RigidBody
	{
		//linear properties
		private Vector2 _mPosition = new Vector2(0f);
		private Vector2 _mVelocity = new Vector2(0f);
		private Vector2 _mForces = new Vector2(0f);
		private float _mMass;

		//angular properties
		private float _mAngle;
		private float _mAngularVelocity;
		private float _mTorque;
		private float _mInertia;

		//graphical properties
		protected Vector2 _mHalfSize = new Vector2();
		private Rectangle _rect = new Rectangle();

		public RigidBody()
		{
			//set these defaults so we dont get divide by zeros
			_mMass = 1.0f;
			_mInertia = 1.0f;
		}

		protected Level Level { get; private set; }
		//intialize out parameters
		public virtual void Setup(Level level, Vector2 halfSize, float mass)
		{
			Level = level;

			//store physical parameters
			_mHalfSize = halfSize;
			_mMass = mass;
			_mInertia = (1.0f / 12.0f) * (halfSize.X * halfSize.X) * (halfSize.Y * halfSize.Y) * mass;

			//generate our viewable rectangle
			_rect.X = (int)-_mHalfSize.X;
			_rect.Y = (int)-_mHalfSize.Y;
			_rect.Width = (int)(_mHalfSize.X * 2.0f);
			_rect.Height = (int)(_mHalfSize.Y * 2.0f);
		}

		public void SetLocation(Vector2 position, float angle)
		{
			_mPosition = position;
			_mAngle = angle;
		}

		public Vector2 GetPosition()
		{
			return _mPosition;
		}

		protected float GetAngle()
		{
			return _mAngle;
		}

		public virtual void Update(float timeStep)
		{
			//integrate physics
			//linear
			Vector2 acceleration = _mForces / _mMass;
			_mVelocity += acceleration * timeStep;

			var positionPreview = _mPosition + (_mVelocity * timeStep);
			if (CanMove(positionPreview))
			{
				_mPosition = positionPreview;
			}
			else
			{
				_mVelocity = -_mVelocity * 0.8f;
			}
			//_mPosition += _mVelocity * timeStep;
			_mForces = new Vector2(0, 0); //clear forces

			//angular
			float angAcc = _mTorque / _mInertia;
			_mAngularVelocity += angAcc * timeStep;
			_mAngle += _mAngularVelocity * timeStep;
			_mTorque = 0; //clear torque
		}

		public void Draw(SpriteBatch sb, Texture2D texture, Camera2D camera)
		{
			sb.Begin(transformMatrix: camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);

			sb.Draw(texture, _mPosition, null, Color.White, _mAngle / (float)Math.PI * 180.0f, _mHalfSize, 1, SpriteEffects.None, 0);

			sb.End();
		}

		//take a relative Vector2 and make it a world Vector2
		public Vector2 RelativeToWorld(Vector2 relative)
		{
			return Vector2.Transform(relative, Matrix.CreateRotationZ(_mAngle / (float)Math.PI * 180.0f));
		}

		//take a world Vector2 and make it a relative Vector2
		public Vector2 WorldToRelative(Vector2 world)
		{
			return Vector2.Transform(world, Matrix.CreateRotationZ(-_mAngle / (float)Math.PI * 180.0f));
		}

		//velocity of a point on body
		public Vector2 PointVel(Vector2 worldOffset)
		{
			Vector2 tangent = new Vector2(-worldOffset.Y, worldOffset.X);
			return tangent * _mAngularVelocity + _mVelocity;
		}

		public void AddForce(Vector2 worldForce, Vector2 worldOffset)
		{
			//add linar force
			_mForces += worldForce;
			//and it's associated torque
			_mTorque += (worldForce.X * worldOffset.Y - worldForce.Y * worldOffset.X);
		}

		public int GetSpeed()
		{
			return (int) Math.Floor(Math.Max(Math.Max(Math.Abs(_mVelocity.X), Math.Abs(_mVelocity.Y)), Math.Abs(_mAngularVelocity)));
		}

		public Vector2 GetVelocity()
		{
			return _mVelocity;
		}

		protected virtual bool CanMove(Vector2 pos)
		{
			return true;
		}
	}
}
