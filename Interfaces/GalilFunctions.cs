using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galil;
namespace Interfaces
{
    class GalilFunctions
    {
        Galil.Galil g;
        public String IPAddress = "192.168.0.31";
        public Boolean connected;
        public bool GalilInit()
        {
            try
            {
                if (connected) return true;
                g = new Galil.Galil();
                g.address = IPAddress;
                g.timeout_ms = 500;
                connected = true;
                g.command("RS");
                g.command("XQ#INIT,0");
                g.command("SH");
                g.command("XQ#HOMEXYZ");
                return true;
            }

            catch (Exception ex)
            {
                //EventErrGalilTriger(ex.Message.ToString());
                return false;
            }
        }

        public double getPosition(int axis_no)
        {
            try
            {
                double pos = 0;
                switch (axis_no)
                {

                    case 0://x-axis
                        pos = g.commandValue("MG_TPX");
                        break;
                    case 1://y-axis
                        pos = g.commandValue("MG_TPY");
                        break;
                    case 2://z-axis
                        pos = g.commandValue("MG_TPZ");
                        break;
                }
                return pos;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public bool homingXYZ()
        {
            try
            {
                g.command("XQ#HOMEXYZ");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool homing(int axis_no)
        {
            try
            {
                switch (axis_no)
                {

                    case 0://x-axis
                        g.commandValue("XQ#HOMEX");
                        break;
                    case 1://y-axis
                        g.commandValue("XQ#HOMEY");
                        break;
                    case 2://z-axis
                        g.commandValue("XQ#HOMEZ");
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool stop(int axis_no)
        {
            try
            {
                switch (axis_no)
                {

                    case 0://x-axis
                        g.commandValue("STX");
                        break;
                    case 1://y-axis
                        g.commandValue("STY");
                        break;
                    case 2://z-axis
                        g.commandValue("STZ");
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool jog(int axis_no, int direction, int speed = 30000, int acc = 300000,  int dcc = 300000)
        {
            String command = "";
            String direct = "";
            try
            {
                command += "JG";
                
                switch (direction)
                {
                    case 0://forward
                        direct = "+";
                        break;
                    case 1://reverse
                        direct = "-";
                        break;
                }
                command += direct + speed + ";";
                command += "ACC=" + acc + ";";
                command += "DCC=" + acc + ";";

                switch (axis_no)
                {

                    case 0://x-axis
                        command += "BGX";
                        break;
                    case 1://y-axis
                        command += "BGY";
                        break;
                    case 2://z-axis
                        command += "BGZ";
                        break;
                }

                g.command(command);
                Console.WriteLine(command);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }


}
