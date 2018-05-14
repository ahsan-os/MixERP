using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Frapid.DataAccess.Subtext
{
    /*****************************************************************************************
    This license is based on the new BSD template found here 
    (http://www.opensource.org/licenses/bsd-license.php)

    -----------------------------------------------------------------
    Subtext (http://subtextproject.com/)
    Copyright (c) 2005 - 2007 
    by Phil Haack (http://haacked.com/)

    XML-RPC.NET Copyright (c) 2006 Charles Cook

    This work contains and builds upon code from the original .TEXT source Code 
    which is Copyright (c) 2003 Scott Watermasysk (http://scottwater.com/blog/), 
    and which is used herein pursuant to the BSD License.

    All rights reserved.

    Redistribution and use in source and binary forms, with or without modification, 
    are permitted provided that the following conditions are met:

        *	Redistributions of source code must retain the above copyright notice, 
            this list of conditions and the following disclaimer.
        *	Redistributions in binary form must reproduce the above copyright notice, 
            this list of conditions and the following disclaimer in the documentation 
            and/or other materials provided with the distribution.
        *	Neither the name of the Subtext nor the names of its contributors 
            may be used to endorse or promote products derived from this software 
            without specific prior written permission.

    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
    ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
    WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
    IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
    INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
    BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, 
    DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY 
    OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE 
    OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED 
    OF THE POSSIBILITY OF SUCH DAMAGE.
    ****************************************************************************************/

    internal class SqlScriptReader : ScriptReader
    {
        public SqlScriptReader(ScriptSplitter splitter) : base(splitter)
        {
        }

        protected override bool ReadNext()
        {
            if (this.EndOfLine) //end of line
            {
                this.Splitter.Append(this.Current);
                this.Splitter.SetParser(new SeparatorLineReader(this.Splitter));
                return false;
            }

            this.Splitter.Append(this.Current);
            return false;
        }
    }

    public class ScriptSplitter: IEnumerable<string>
    {
        private readonly TextReader _reader;
        private StringBuilder _builder = new StringBuilder();
        private ScriptReader _scriptReader;

        public ScriptSplitter(string script)
        {
            this._reader = new StringReader(script);
            this._scriptReader = new SeparatorLineReader(this);
        }

        internal bool HasNext => this._reader.Peek() != -1;

        internal char Current { get; private set; } = char.MinValue;

        internal char LastChar { get; private set; } = char.MinValue;

        public IEnumerator<string> GetEnumerator()
        {
            while(this.Next())
            {
                if(this.Split())
                {
                    string script = this._builder.ToString().Trim();
                    if(script.Length > 0)
                        yield return script;
                    this.Reset();
                }
            }
            if(this._builder.Length > 0)
            {
                string scriptRemains = this._builder.ToString().Trim();
                if(scriptRemains.Length > 0)
                    yield return scriptRemains;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        internal bool Next()
        {
            if(!this.HasNext)
            {
                return false;
            }

            this.LastChar = this.Current;
            this.Current = (char)this._reader.Read();
            return true;
        }

        internal int Peek()
        {
            return this._reader.Peek();
        }

        private bool Split()
        {
            return this._scriptReader.ReadNextSection();
        }

        internal void SetParser(ScriptReader newReader)
        {
            this._scriptReader = newReader;
        }

        internal void Append(string text)
        {
            this._builder.Append(text);
        }

        internal void Append(char c)
        {
            this._builder.Append(c);
        }

        private void Reset()
        {
            this.Current = this.LastChar = char.MinValue;
            this._builder = new StringBuilder();
        }
    }
}