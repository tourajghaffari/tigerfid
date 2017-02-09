# Application Programming Interface - Language: Java

***Originally written by Touraj Ghaffari***

## Introduction

The API functions may execute in a synchronous or asynchronous manner. Most of API functions will execute asynchronously. The following descriptions and diagram illustrate several possible scenarios that may result from an API function call.

1. The function returns `RF_S_DONE` indicating that a valid response was sent either by API or the device and this is the final return value for the function call sent to API.

2. The function returns `RF_E_XXX` indicating an error condition. The specific return value identifies the cause of the error. Any output arguments are not guaranteed to be valid.

3. The function returns `RF_S_PEND` indicating that it will be executed asynchronously. The application should expect one or more callbacks corresponding to this function. A callback with `RF_S_OK_PEND` status indicates the function sent to API was a broadcast command and this return value is a device response to this command. There might be more responses from other devices. The last callback will have `RF_S_DONE` status indicating that it is the last callback for the function. 

4. The function returns `RF_S_PEND` indicating that it will be executed asynchronously. A callback with `RF_S_DONE` status indicates the function that was sent to API was executed successfully and this is the last response for this function from API.

5. The function returns `RF_S_PEND` indicating that it will be executed asynchronously. A callback with `RF_E_XXX` status indicates the function that there was an error executing this command.   

All functions will have a unique packet ID. The value of this argument will be passed to the callback function. The application can use this value to match the callback to originating function call.

## Commands

### rfScanNetwork

`rfScanNetwork` searches the network for any reader with active network connection.

`public int rfScanNetwork(int pktID)`

#### Parameters

`pktID` - Packet identifier used to match callbacks to a specific function call.

#### Return Values

* `RF_S_PEND` if function acknowledged by API.

* `RF_S_OK_PEND` if function acknowledged and responded successfully each by each reader or API.

* `RF_S_DONE` if function successful and acknowledged by reader, repeater or API.

* Error code otherwise.

`rfReaderEvnt_t` structure in reader call back function contains information about the IP address of the reader. The table below describes witch items in the `rfReaderEvent_t` structure are valid for this function.

Items | Valid
--- | ---
host | Yes
reader | No
repeater | No
IP | Yes
port | Yes
relay | No
fGenerator | No
eventType | Yes
cmdType | Yes
eventStatus | Yes
errorStatus | Yes
pktID | Yes
cmdRef | No
data | No
versionInfo | No

### rfOpenSocket

#### Parameters

#### Return Values

### rfScanIP

#### Parameters

#### Return Values

### rfCloseSocket

#### Parameters

#### Return Values

### rfResetReader

#### Parameters

#### Return Values

### rfQueryReader

#### Parameters

#### Return Values

### rfRegisterReaderEvent

#### Parameters

### rfQueryTags

#### Parameters

#### Return Values

### rfReadTags

#### Parameters

#### Return Values

### rfWriteTags

#### Parameters

#### Return Values

### rfConfigureTags

#### Parameters

#### Return Values

### rfRegisterTagEvent

#### Parameters

## Unsolicited Event Messages

### RF_TAG_DETECTED

### RF_INVALID_PACKET

## Structures (Data Types)

### rfVersionInfo_t

#### Attributes

### rfReaderEvent_t

#### Attributes

### rfTagEvent_t

#### Attributes

### rfTagSelect_t

#### Attributes

### rfTagStatus_t

#### Attributes

### rfTagTemp_t

#### Attributes

### rfTag_t

#### Attributes

### rfNewTagConfig_t

#### Attributes
