using System;
using System.Collections.Generic;
using System.Text;
using AWIComponentLib;

namespace AWIComponentLib.Actions
{    
    class HW_ActionClass 
    {
        ushort reader = 0;
        int actionType = 0;
        int duration = 0;

        HW_ActionClass() {}

        HW_ActionClass(ushort rdr, int type, ushort dur)
        {
            actionType = type;
            duration = dur;
            reader = rdr;
            
            DoAction();
        }

        private void DoAction()
        {
            //At this time autoMode is set to true internally so there is no
            //need to close the relay port manualy. the port will close after
            //duration time.

            switch (actionType)
            {
                case ConstantsClass.OPEN_RELAY_01:
                    AddOutputRelay(reader, 01, duration);
                break;

                case ConstantsClass.OPEN_RELAY_02:
                    AddOutputRelay(reader, 02, duration);
                break;

                case ConstantsClass.Close_RELAY_01:
                break;

                case ConstantsClass.Close_RELAY_02:
                break;
           }

        }

    }
}
