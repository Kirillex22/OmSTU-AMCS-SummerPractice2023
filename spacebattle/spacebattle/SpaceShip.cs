﻿using System.Collections.Concurrent;
namespace spacebattle;

public class SpaceShip
{
    private double x; 
    private double y; 
    private double Vx; 
    private double Vy; 
    private double fuel; 
    private double yaw;
    private double yaw_speed;
    private bool move_status = true;
    private bool spin_status = true;
    private double fuel_unit;
    private double eps = 1e-5;

    public void SetCoords(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public void SetSpeed(double Vx, double Vy)
    {
        this.Vx = Vx;
        this.Vy = Vy;
    }

    public void SetMoveStatus(bool move_status)
    {
        this.move_status = move_status;
    }

    public void Refuel(double fuel)
    {
        this.fuel = fuel;
    }

    public void SetYaw(double yaw)
    {
        this.yaw = yaw;
    }

    public void SetYawSpeed(double yaw_speed)
    {
        this.yaw_speed = yaw_speed;
    }

    public void SetSpinStatus(bool spin_status)
    {
        this.spin_status = spin_status;
    }

    public void SetFuelUnit(double fuel_unit)
    {
        this.fuel_unit = fuel_unit;
    }

    public double GetFuelCount()
    {
        return fuel;
    }

    public double[] Move(double t = 1)
    {
        if ((x == double.NaN)||(y == double.NaN))
            throw new Exception();

        else if ((Vx == double.NaN)||(Vy == double.NaN))
            throw new Exception();

        else if(!move_status)
            throw new Exception();
        
        else if (Math.Abs(fuel - fuel_unit) < eps)
        {
            move_status = false;
            throw new Exception();
        }
            
        x += Vx*t;
        y += Vy*t;
        fuel -= fuel_unit*t;

        var CurrCoords = new double[] {x, y};

        return CurrCoords;
        
    }

    public double Spin(double t = 1)
    {
        if (yaw == double.NaN)
            throw new Exception();

        else if (yaw_speed == double.NaN)
            throw new Exception();

        else if(!spin_status)
            throw new Exception();
        
        yaw += yaw_speed*t;

        if (yaw >= 360)
            yaw -= 360;

        return yaw;
    }

     public class Pool<T>
    {
        private readonly ConcurrentBag<T> _objects;
        private readonly Func<T> _objectGenerator;

        public Pool(Func<T> objectGenerator)
        {
            _objectGenerator = objectGenerator ?? throw new ArgumentNullException(nameof(objectGenerator));
            _objects = new ConcurrentBag<T>();
        }

        public T Get() => _objects.TryTake(out T item) ? item : _objectGenerator();

        public void Return(T item)
        {
            _objects.Add(item);
        } 
    }

    public class PoolGuard<T> : IDisposable
    {
        private T object_;
        private Pool<T> pool;
        private bool _dispValue = false;
        public PoolGuard(Pool<T> pool)
        {
            this.pool = pool;
            this.object_ = pool.Get();
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this._dispValue)
            {
                if (disposing) pool.Return(object_);
            }
            _dispValue = true;
        }
        ~PoolGuard() 
        {
             Dispose(disposing: false); 
        }
    }

}
