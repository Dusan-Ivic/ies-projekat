using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class Clamp : ConductingEquipment
    {
        private float lengthFromTerminal1;

        private long acLineSegment;

        public Clamp(long globalId) : base(globalId)
        {
        }

        public float LengthFromTerminal1
        {
            get { return lengthFromTerminal1; }
            set { lengthFromTerminal1 = value; }
        }

        public long ACLineSegment
        {
            get { return acLineSegment; }
            set { acLineSegment = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Clamp x = (Clamp)obj;
                return (x.lengthFromTerminal1 == this.lengthFromTerminal1 && x.acLineSegment == this.acLineSegment);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.CLAMP_LENGTHFROMTERMINAL:
                case ModelCode.CLAMP_ACLINESEGMENT:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.CLAMP_LENGTHFROMTERMINAL:
                    prop.SetValue(lengthFromTerminal1);
                    break;

                case ModelCode.CLAMP_ACLINESEGMENT:
                    prop.SetValue(acLineSegment);
                    break;

                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.CLAMP_LENGTHFROMTERMINAL:
                    lengthFromTerminal1 = property.AsFloat();
                    break;

                case ModelCode.CLAMP_ACLINESEGMENT:
                    acLineSegment = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (acLineSegment != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.CLAMP_ACLINESEGMENT] = new List<long>();
                references[ModelCode.CLAMP_ACLINESEGMENT].Add(acLineSegment);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation
    }
}
