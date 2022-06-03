using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entitati {
    public class ListaGen<T> : IEnumerable<T> where T : IComparable<T> {
        private class Nod {
            private T? data;
            private Nod? next;

            public Nod(T? data) => this.data = data;

            public T? Data { get => data; set => data = value; }
            public ListaGen<T>.Nod? Next { get => next; set => next = value; }
        }

        private Nod? start, end;
        private Int32 count;
        public ListaGen() { end = start = null; this.count = 0; }

        public void PushFront(T t) {
            Nod n = new(t) {
                Next = start
            };
            start = n;
            if (end == null) end = n;
            count++;
        }

        public void PushBack(T t) {
            Nod n = new(t);
            if (start == null) start = n;
            if (end != null) end.Next = n;
            end = n;
            count++;
        }

        public void Add(T t) => PushBack(t);

        public void Delete(T t) {
            Nod? current = start;
            while (current != null && current.Data!.Equals(t)) {
                start = start!.Next;
                current = start;
            }
            if (current == null) return;
            while (current.Next != null) {
                if (current.Next.Data!.Equals(t)) {
                    if (end == current.Next) {
                        end = current;
                        end.Next = null;
                    }
                    else {
                        Nod del = current.Next;
                        current.Next = current.Next.Next;
                        del.Next = null;
                    }
                }
                else current = current.Next;
            }
            count--;
        }

        public IEnumerator<T> GetEnumerator() {
            Nod? current = start;
            while (current != null) {
                yield return current.Data!;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            Nod? current = start;
            while (current != null) {
                yield return current.Data!;
                current = current.Next;
            }
        }

        public void Sort() {
            throw new NotImplementedException("Sortarea nu este implementată pentru ListaGen.");
        }

        public Int32 Count { get => count; }
    }
}
