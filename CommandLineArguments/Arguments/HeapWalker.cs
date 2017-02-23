using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Diagnostics.Runtime;
using System.Diagnostics;

namespace Arguments
{
    internal static class HeapWalker
    {
        public static IEnumerable<WeakReference<object>> GetHeapObjects(uint msecTimeout)
        {
            DataTarget dataTarget = DataTarget.AttachToProcess(Process.GetCurrentProcess().Id, msecTimeout);

            return dataTarget.ClrVersions.Select(x => x.CreateRuntime().GetHeap()).Select(heap =>
            {
                return heap.Segments.Select(seg =>
                {
                    List<WeakReference<object>> instances = new List<WeakReference<object>>();

                    for (ulong obj = seg.FirstObject; obj != 0; obj = seg.NextObject(obj))
                    {
                        instances.Add(new WeakReference<object>(heap.GetObjectType(obj).GetValue(obj)));
                    }

                    return instances.AsEnumerable();
                }).Aggregate((x, y) => x.Concat(y));
            }).Aggregate((x, y) => x.Concat(y));
        }
    }
}
