using System;
using System.Collections.Generic;
using System.Text;

namespace FTN.Common
{
	
	public enum DMSType : short
	{		
		MASK_TYPE							= unchecked((short)0xFFFF),
        
        TERMINAL                            = 0x0001,
        CONNECTIVITY_NODE                   = 0x0002,
        CLAMP                               = 0x0003,
        RECTIFIER_INVERTER                  = 0x0004,
        ACLINE_SEGMENT                      = 0x0005,
	}

    [Flags]
	public enum ModelCode : long
	{
        IDOBJ                               = 0x1000000000000000,
        IDOBJ_GID                           = 0x1000000000000104,
        IDOBJ_ALIAS                         = 0x1000000000000207,
        IDOBJ_MRID                          = 0x1000000000000307,
        IDOBJ_NAME                          = 0x1000000000000407,

        PSR                                 = 0x1100000000000000,

        TERMINAL                            = 0x1200000000010000,
        TERMINAL_CONNECTED                  = 0x1200000000010101,
        TERMINAL_PHASES                     = 0x120000000001020a,
        TERMINAL_SEQNUM                     = 0x1200000000010303,
        TERMINAL_CONDEQ                     = 0x1200000000010409,
        TERMINAL_NODE                       = 0x1200000000010509,

        CONNECTIVITYNODE                    = 0x1300000000020000,
        CONNECTIVITYNODE_TERMINALS          = 0x1300000000020119,

        EQUIPMENT                           = 0x1110000000000000,

        CONDEQ                              = 0x1111000000000000,
        CONDEQ_TERMINALS                    = 0x1111000000000119,

        CONDUCTOR                           = 0x1111100000000000,

        CLAMP                               = 0x1111200000030000,
        CLAMP_LENGTHFROMTERMINAL            = 0x1111200000030105,
        CLAMP_ACLINESEGMENT                 = 0x1111200000030209,

        RECTIFIER_INVERTER                  = 0x1111300000040000,

        ACLINESEGMENT                       = 0x1111110000050000,
        ACLINESEGMENT_CLAMP                 = 0x1111110000050119,
    }

    [Flags]
	public enum ModelCodeMask : long
	{
		MASK_TYPE			 = 0x00000000ffff0000,
		MASK_ATTRIBUTE_INDEX = 0x000000000000ff00,
		MASK_ATTRIBUTE_TYPE	 = 0x00000000000000ff,

		MASK_INHERITANCE_ONLY = unchecked((long)0xffffffff00000000),
		MASK_FIRSTNBL		  = unchecked((long)0xf000000000000000),
		MASK_DELFROMNBL8	  = unchecked((long)0xfffffff000000000),		
	}																		
}


