using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB
{
internal class FeatureData
{
    // Fields
    private string[] t;
    private string v0;
    private string v1;
    private string v2;
    private string v3;

    // Methods
    public FeatureData()
    {
    }

    public FeatureData(string s)
    {
        this.v0 = s;
        this.Count = 1;
    }

    public void Add(FeatureData t)
    {
        for (int i = 0; i < t.Count; i++)
        {
            this[this.Count] = t[i];
        }
    }

    public void Add(string s)
    {
        this[this.Count] = s;
    }

    // Properties
    public int Count {get;set;}

    public string this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return this.v0;

                case 1:
                    return this.v1;

                case 2:
                    return this.v2;

                case 3:
                    return this.v3;
            }
            index -= 4;
            if ((this.t != null) && (index < this.t.Length))
            {
                return this.t[index];
            }
            return null;
        }
        set
        {
            if ((index + 1) >= this.Count)
            {
                this.Count = index + 1;
            }
            switch (index)
            {
                case 0:
                    this.v0 = value;
                    return;

                case 1:
                    this.v1 = value;
                    return;

                case 2:
                    this.v2 = value;
                    return;

                case 3:
                    this.v3 = value;
                    return;
            }
            index -= 4;
            if ((this.t == null) || (index >= this.t.Length))
            {
                int num = ((index / 0x10) + 1) * 0x10;
                if (this.t == null)
                {
                    this.t = new string[num];
                }
                else
                {
                    string[] destinationArray = new string[Math.Max(num, 2 * this.t.Length)];
                    Array.Copy(this.t, destinationArray, this.t.Length);
                    this.t = destinationArray;
                }
            }
            this.t[index] = value;
        }
    }
} 
}
