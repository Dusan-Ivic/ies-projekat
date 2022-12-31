using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Terminal : IdentifiedObject
    {
        private bool connected;

        private PhaseCode phases;

        private int sequenceNumber;

        private long conductingEquipment;

        private long connectivityNode;

        public Terminal(long globalId) : base(globalId)
        {
        }

        public bool Connected
        {
            get { return connected; }
            set { connected = value; }
        }

        public PhaseCode Phases
        {
            get { return phases; }
            set { phases = value; }
        }

        public int SequenceNumber
        {
            get { return sequenceNumber; }
            set { sequenceNumber = value; }
        }

        public long ConductingEquipment
        {
            get { return conductingEquipment; }
            set { conductingEquipment = value; }
        }

        public long ConnectivityNode
        {
            get { return connectivityNode; }
            set { connectivityNode = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Terminal x = (Terminal)obj;
                return (x.connected == this.connected && x.phases == this.phases && x.sequenceNumber == this.sequenceNumber &&
                        x.conductingEquipment == this.conductingEquipment && x.connectivityNode == this.connectivityNode);
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
                case ModelCode.TERMINAL_CONNECTED:
                case ModelCode.TERMINAL_PHASES:
                case ModelCode.TERMINAL_SEQNUM:
                case ModelCode.TERMINAL_CONDEQ:
                case ModelCode.TERMINAL_NODE:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.TERMINAL_CONNECTED:
                    prop.SetValue(connected);
                    break;

                case ModelCode.TERMINAL_PHASES:
                    prop.SetValue((short)phases);
                    break;

                case ModelCode.TERMINAL_SEQNUM:
                    prop.SetValue(sequenceNumber);
                    break;

                case ModelCode.TERMINAL_CONDEQ:
                    prop.SetValue(conductingEquipment);
                    break;

                case ModelCode.TERMINAL_NODE:
                    prop.SetValue(connectivityNode);
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
                case ModelCode.TERMINAL_CONNECTED:
                    connected = property.AsBool();
                    break;

                case ModelCode.TERMINAL_PHASES:
                    phases = (PhaseCode)property.AsEnum();
                    break;

                case ModelCode.TERMINAL_SEQNUM:
                    sequenceNumber = property.AsInt();
                    break;

                case ModelCode.TERMINAL_CONDEQ:
                    conductingEquipment = property.AsReference();
                    break;

                case ModelCode.TERMINAL_NODE:
                    connectivityNode = property.AsReference();
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
            if (conductingEquipment != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_CONDEQ] = new List<long>();
                references[ModelCode.TERMINAL_CONDEQ].Add(conductingEquipment);
            }

            if (connectivityNode != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_NODE] = new List<long>();
                references[ModelCode.TERMINAL_NODE].Add(connectivityNode);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation
    }
}
