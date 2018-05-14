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

    internal class SeparatorLineReader : ScriptReader
    {
        private StringBuilder _builder = new StringBuilder();
        private bool _foundGo;
        private bool _gFound;

        public SeparatorLineReader(ScriptSplitter splitter) : base(splitter)
        {
        }

        private void Reset()
        {
            this._foundGo = false;
            this._gFound = false;
            this._builder = new StringBuilder();
        }

        protected override bool ReadDashDashComment()
        {
            if (!this._foundGo)
            {
                base.ReadDashDashComment();
                return false;
            }
            base.ReadDashDashComment();
            return true;
        }

        protected override void ReadSlashStarComment()
        {
            if (this._foundGo)
                throw new SqlParseException("Incorrect syntax was encountered while parsing GO. Cannot have a slash star /* comment */ after a GO statement.");
            base.ReadSlashStarComment();
        }

        protected override bool ReadNext()
        {
            if (this.EndOfLine) //End of line or script
            {
                if (!this._foundGo)
                {
                    this._builder.Append(this.Current);
                    this.Splitter.Append(this._builder.ToString());
                    this.Splitter.SetParser(new SeparatorLineReader(this.Splitter));
                    return false;
                }
                this.Reset();
                return true;
            }

            if (this.WhiteSpace)
            {
                this._builder.Append(this.Current);
                return false;
            }

            if (!this.CharEquals('g') &&
                !this.CharEquals('o'))
            {
                this.FoundNonEmptyCharacter(this.Current);
                return false;
            }

            if (this.CharEquals('o'))
            {
                if (CharEquals('g', this.LastChar) &&
                    !this._foundGo)
                    this._foundGo = true;
                else
                    this.FoundNonEmptyCharacter(this.Current);
            }

            if (CharEquals('g', this.Current))
            {
                if (this._gFound ||
                    (!char.IsWhiteSpace(this.LastChar) && this.LastChar != char.MinValue))
                {
                    this.FoundNonEmptyCharacter(this.Current);
                    return false;
                }

                this._gFound = true;
            }

            if (!this.HasNext &&
                this._foundGo)
            {
                this.Reset();
                return true;
            }

            this._builder.Append(this.Current);
            return false;
        }

        private void FoundNonEmptyCharacter(char c)
        {
            this._builder.Append(c);
            this.Splitter.Append(this._builder.ToString());
            this.Splitter.SetParser(new SqlScriptReader(this.Splitter));
        }
    }
}