using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Utilities
{
    public class DecodeResult
    {
        private UInt64 Position;
	    private Object Decoded;

	    public DecodeResult(UInt64 position, Object decoded) {
            this.Position = position;
		    this.Decoded = decoded;
	    }

	    public UInt64 GetPosition() {
            return Position;
	    }
	    public Object GetDecoded() 
        {
		    return Decoded;
	    }
    }
}
