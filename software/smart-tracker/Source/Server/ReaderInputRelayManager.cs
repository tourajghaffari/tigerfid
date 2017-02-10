using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace AWI.SmartTracker
{
    class ReaderInputRelayManager
    {
        private static volatile ReaderInputRelayManager instance;
        private static object syncRoot = new Object();
        private Dictionary<ReaderFGenRelayTuple, InputRelayInfo> relays;

        private ReaderInputRelayManager()
        {
            relays = new Dictionary<ReaderFGenRelayTuple, InputRelayInfo>();
        }

        private static ReaderInputRelayManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ReaderInputRelayManager();
                    }
                }

                return instance;
            }
        }

        public static void Add(ushort reader, ushort fgen, InputType relay, ushort duration, string description, actionItemStruct[] actions, uint tag_id, TagType tag_type)
        {
            Instance.relays.Add(new ReaderFGenRelayTuple(reader, fgen, relay), new InputRelayInfo(reader, fgen, relay, duration, description, actions, tag_id, tag_type));
        }

        public static void Add(ushort reader, ushort fgen, InputType relay, ushort duration, string description, actionItemStruct[] actions, uint tag_id, byte tag_type)
        {
            Instance.relays.Add(new ReaderFGenRelayTuple(reader, fgen, relay), new InputRelayInfo(reader, fgen, relay, duration, description, actions, tag_id, (TagType)tag_type));
        }

        public static void Add(ushort reader, ushort fgen, InputType relay, ushort duration, string description, actionItemStruct[] actions, ushort tag_id, TagType tag_type)
        {
            Instance.relays.Add(new ReaderFGenRelayTuple(reader, fgen, relay), new InputRelayInfo(reader, fgen, relay, duration, description, actions, tag_id, tag_type));
        }

        public static void Add(ushort reader, ushort fgen, InputType relay, ushort duration, string description, actionItemStruct[] actions, ushort tag_id, byte tag_type)
        {
            Instance.relays.Add(new ReaderFGenRelayTuple(reader, fgen, relay), new InputRelayInfo(reader, fgen, relay, duration, description, actions, tag_id, (TagType)tag_type));
        }

        public static void Add(ushort reader, ushort fgen, InputType relay, ushort duration, string description, actionItemStruct[] actions, ushort tag_id, int tag_type)
        {
            Instance.relays.Add(new ReaderFGenRelayTuple(reader, fgen, relay), new InputRelayInfo(reader, fgen, relay, duration, description, actions, tag_id, (TagType)tag_type));
        }

        public static void Remove(ushort reader, ushort fgen, InputType relay)
        {
            ReaderFGenRelayTuple id = new ReaderFGenRelayTuple(reader, fgen, relay);

            if (instance.relays.ContainsKey(id)) {
                Instance.relays[id].Stop();

                Instance.relays.Remove(id);
            }
        }

        private static void ElapsedEventHandler(object sender, ElapsedEventArgs e)
        {
            InputRelayInfo info = (InputRelayInfo)sender;
            ReaderFGenRelayTuple id = new ReaderFGenRelayTuple(info.Reader, info.FGen, info.Relay);

            if (Timeout != null)
            {
                Timeout(info.Reader, info.FGen, info.Relay, info.Description, e.SignalTime, info.Actions, info.TagID, info.TagType);
            }

            Instance.relays.Remove(id);
        }

        public static event ReaderInputRelayElapsedHandler Timeout;

        private class InputRelayInfo : System.Timers.Timer
        {
            private ushort reader;
            private ushort fgen;
            private InputType relay;
            private string description;
            private actionItemStruct[] actions;
            private uint tag_id;
            private TagType tag_type;

            public InputRelayInfo(ushort reader, ushort fgen, InputType relay, ushort duration, string description, actionItemStruct[] actions, uint tag_id, TagType tag_type)
            {
                this.reader = reader;
                this.relay = relay;
                this.description = description;
                this.actions = actions;
                this.tag_id = tag_id;
                this.tag_type = tag_type;

                base.AutoReset = false;
                base.Interval = duration * 1000;
                base.Enabled = true;
                base.Elapsed += new System.Timers.ElapsedEventHandler(ReaderInputRelayManager.ElapsedEventHandler);
                base.Start();
            }

            public ushort Reader { get { return reader; } }
            public ushort FGen { get { return fgen; } }
            public InputType Relay { get { return relay; } }
            public string Description { get { return description; } }
            public actionItemStruct[] Actions { get { return actions; } }
            public uint TagID { get { return tag_id; } }
            public TagType TagType { get { return tag_type; } }
        }

        private struct ReaderFGenRelayTuple
        {
            ushort reader;
            ushort fgen;
            InputType relay;

            const long RAND_LCG_PARAM_M = 0x7FFFFFFF;
            const long RAND_LCG_PARAM_A = 1103515245;
            const long RAND_LCG_PARAM_B = 12345;

            public ReaderFGenRelayTuple(ushort reader, ushort fgen, InputType relay)
            {
                this.reader = reader;
                this.fgen = fgen;
                this.relay = relay;
            }

            public override int GetHashCode()
            {
                long seed = (reader << 16) + (fgen << 2) + (byte)relay;
                long lcg = ((RAND_LCG_PARAM_A * seed) + RAND_LCG_PARAM_B) % (RAND_LCG_PARAM_M + 1u);
                int hash = (int)lcg;
                return hash;
            }

            public override bool Equals(object obj)
            {
                if (obj is ReaderFGenRelayTuple)
                {
                    ReaderFGenRelayTuple compare = (ReaderFGenRelayTuple)obj;
                    if ((compare.reader == reader) && (compare.fgen == fgen) && (compare.relay == relay))
                        return true;
                    else
                        return false;
                }
                return base.Equals(obj);
            }
        }
    }

    public enum InputType : byte
    {
        Input1 = 1,
        Input2 = 2
    }

    public delegate void ReaderInputRelayElapsedHandler(ushort reader, ushort fgen, InputType relay, string description, DateTime time, actionItemStruct[] actions, uint tag_id, TagType tag_type);
}
