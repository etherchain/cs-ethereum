using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LevelDB;

namespace Ethereum.Utilities
{
    public static class Trie
    {
        public static void GetHelper(Object node, Object key)
        {
            //    if key == []: return node
            //    if node = '': return ''
            //    curnode = rlp.decode(node if len(node) < 32 else db.get(node))
            //    if len(curnode) == 2:
            //        (k2, v2) = curnode
            //        k2 = compact_decode(k2)
            //        if k2 == key[:len(k2)]:
            //            return get(v2, key[len(k2):])
            //        else:
            //            return ''
            //    elif len(curnode) == 17:
            //        return get_helper(curnode[key[0]],key[1:])
        }

        public static void Get(Object node, Object key)
        {
            //    key2 = []
            //    for i in range(len(key)):
            //        key2.push(int(ord(key) / 16))
            //        key2.push(ord(key) % 16)
            //    key2.push(16)
            //    return get_helper(node,key2)
        }
    }
}
