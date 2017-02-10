#define RESET_DEVICE                 0x01
#define RESET_DEVICE_ACK             0x81

#define ENABLE_RELAY                 0x02
#define ENABLE_RELAY_ACK             0x82

#define DISABLE_RELAY                0x03
#define DISABLE_RELAY_ACK            0x83

#define ENABLE_READER                0x04
#define ENABLE_READER_ACK            0x84

#define DISABLE_READER               0x05
#define DISABLE_READER_ACK           0x85

#define CONFIG_TAG                   0x06
#define CONFIG_TAG_ACK               0x86

#define ENABLE_TAG                   0x07
#define ENABLE_TAG_ACK               0x87

#define DISABLE_TAG                  0x08
#define DISABLE_TAG_ACK              0x88

#define QUERY_TAG                    0x09
#define QUERY_TAG_ACK                0x89

#define INPUT_STATUS                 0x11
#define INPUT_STATUS_ACK             0x91

#define CALL_TAG                     0x0A
#define CALL_TAG_ACK                 0x1A

#define READER_CODE_VER              0x0D
#define READER_CODE_VER_ACK          0x8D

#define ASSIGN_READER                0x0E
#define ASSIGN_READER_ACK            0x8E

#define QUERY_READER                 0x0F
#define QUERY_READER_ACK             0x8F

#define DEFINE_TAG_READER            0x10
#define DEFINE_TAG_READER_ACK        0x90

#define CONFIG_TAG_RND               0x12
#define CONFIG_TAG_RND_ACK           0x92

#define CONFIG_FIELD_GEN             0x20
#define CONFIG_FIELD_GEN_ACK         0xA0

#define CONFIG_TX_TIME               0x20
#define CONFIG_TX_TIME_ACK           0xA0

#define QUERY_FIELD_GEN              0x21
#define GET_READER_CONFIG            0x21
#define QUERY_FIELD_GEN_ACK          0xA1

#define SET_READER_DPOT              0x22
#define SET_READER_DPOT_ACK          0xA2

#define CALL_TAG_SMART_FGEN          0x2A
#define CALL_TAG_SMART_FGEN_ACK      0xAA

#define POWER_UP                     0x30
#define POWER_UP_ACK                 0xB0

#define TAG_DETECTED                 0x31
#define TAG_DETECTED_RSSI            0x32
#define TAG_DETECTED_ACK             0xB1

#define GENERAL_COMMAND              0x39

#define WRITE_TAG_MEMORY             0x0B
#define WRITE_TAG_MEMORY_ACK         0x8B

#define READ_TAG_MEMORY              0x0C
#define READ_TAG_MEMORY_ACK          0x8C

#define READ_CONFIG_TAG_MEMORY       0x50
#define WRITE_CONFIG_TAG_MEMORY      0x51

#define ENABLE_FIELD_GEN             0x6B
#define ENABLE_FIELD_GEN_ACK         0xEB

#define BOOT_QUERY                   0x6F

#define TAG_DETECTED_UNSOLICITED     0x75

#define INVALID_RESPONSE             0xFF

#define READ_TAG_TEMP_CONFIG         0x90
#define READ_TAG_TEMPERATURE         0x91
#define WRITE_TAG_TEMP_CONFIG        0x92
#define RESTART_TAG_TEMP_PERIODIC    0x93
#define WRITE_TAG_LOG_TIME           0x94

#define RESET_SMART_FGEN             0x95
#define CONFIG_SMART_FIELD_GEN       0x96
#define QUERY_SMART_FIELD_GEN        0x97
#define QUERY_PROC_SMART_FIELD_GEN   0x98
#define GET_DPOT_SMART_FGEN          0x99
#define INC_DPOT_SMART_FGEN          0x9A  //0x11
#define DEC_DPOT_SMART_FGEN          0x9B  //0x12
#define ABS_DPOT_SMART_FGEN          0x9C  //0x13
#define SET_CONFIG_TAG_LED           0xC9  //201
#define GET_CONFIG_TAG_LED           0xCA  //202
#define GET_CONFIG_TAG_SPEAKER       0xCB  //203
#define SET_CONFIG_TAG_SPEAKER       0xCC  //204

#define LAST_COMMAND                 0xFF
//#define INIT_READER                0x01
#define SET_READER_ID                0x01
#define TOGGLE_ASCII_MODE            0x0A  //'a'
#define FORCE_BAD_CRC                0x0B  //'b'
#define DUMP_READER_RAM              0x0D  //'d'
#define SETUP_READER_ID              0x00  //'i'
#define LOCK_DOOR                    0x00  //'l'
#define NORMAL_MODE                  0x00  //'n'
#define QUITE_MODE                   0x00  //'q'
#define UNLOCK_DOOR                  0x00  //'u'
#define TOGGLE_QUITE_MODE            0x00  //' '
#define PROGRAM_TAG                  0xE1

#define DIAGNOSTIC                   0xDC
#define READ_TAG                     0xAC

 