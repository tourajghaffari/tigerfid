using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace AWI.SmartTracker
{
    public sealed class ReaderOutputRelayManager
    {
        private static volatile ReaderOutputRelayManager instance;
        private static object syncRoot = new Object();
        private List<OuputRelayInfo> relays;

        private ReaderOutputRelayManager()
        {
            relays = new List<OuputRelayInfo>();
        }

        private static ReaderOutputRelayManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ReaderOutputRelayManager();
                    }
                }

                return instance;
            }
        }

        public static void Add(ushort reader, ushort relay, ushort duration, string description, uint tag_id, TagType tag_type)
        {
            Instance.relays.Add(new OuputRelayInfo(reader, relay, duration, description, tag_id, tag_type));
        }

        public static void Add(ushort reader, ushort relay, ushort duration, string description, uint tag_id, byte tag_type)
        {
            Instance.relays.Add(new OuputRelayInfo(reader, relay, duration, description, tag_id, (TagType)tag_type));
        }

        public static void Add(ushort reader, ushort relay, ushort duration, string description, ushort tag_id, TagType tag_type)
        {
            Instance.relays.Add(new OuputRelayInfo(reader, relay, duration, description, tag_id, tag_type));
        }

        public static void Add(ushort reader, ushort relay, ushort duration, string description, ushort tag_id, byte tag_type)
        {
            Instance.relays.Add(new OuputRelayInfo(reader, relay, duration, description, tag_id, (TagType)tag_type));
        }

        private static void ElapsedEventHandler(object sender, ElapsedEventArgs e)
        {
            OuputRelayInfo info = (OuputRelayInfo)sender;

            if (Elapsed != null)
            {
                Elapsed(info.Reader, info.Relay, info.Description, e.SignalTime, info.TagID, info.TagType);
            }

            Instance.relays.Remove(info);
        }

        public static event ReaderOutputRelayElapsedHandler Elapsed;

        private class OuputRelayInfo : System.Timers.Timer
        {
            private ushort reader;
            private ushort relay;
            private string description;
            private uint tag_id;
            private TagType tag_type;

            public OuputRelayInfo(ushort reader, ushort relay, ushort duration, string description, uint tag_id, TagType tag_type)
            {
                this.reader = reader;
                this.relay = relay;
                this.description = description;
                this.tag_id = tag_id;
                this.tag_type = tag_type;

                base.AutoReset = false;
                base.Interval = duration * 1000;
                base.Enabled = true;
                base.Elapsed += new System.Timers.ElapsedEventHandler(ReaderOutputRelayManager.ElapsedEventHandler);
                base.Start();
            }

            public ushort Reader { get { return reader; } }
            public ushort Relay { get { return relay; } }
            public string Description { get { return description; } }
            public uint TagID { get { return tag_id; } }
            public TagType TagType { get { return tag_type; } }
        }
    }

    public delegate void ReaderOutputRelayElapsedHandler(ushort reader, ushort relay, string description, DateTime time, uint tag_id, TagType tag_type);

    public enum TagType : byte
    {
        Access = 1,
        Inventory = 2,
        Asset = 3
    }

#warning Not Used
    public struct ReaderRelayPair
    {
        ushort reader;
        ushort relay;

        public ReaderRelayPair(ushort reader, ushort relay)
        {
            this.reader = reader;
            this.relay = relay;
        }

        public override int GetHashCode()
        {
            return (reader << 16) | relay;
        }

        public override bool Equals(object obj)
        {
            if (obj is ReaderRelayPair)
            {
                ReaderRelayPair compare = (ReaderRelayPair)obj;
                if ((compare.reader == reader) && (compare.relay == relay))
                    return true;
                else
                    return false;
            }
            return base.Equals(obj);
        }
    }

}
