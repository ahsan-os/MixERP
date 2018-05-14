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

    internal abstract class ScriptReader
    {
        protected readonly ScriptSplitter Splitter;

        protected ScriptReader(ScriptSplitter splitter)
        {
            this.Splitter = splitter;
        }

        /// <summary>
        ///     This acts as a template method. Specific Reader instances
        ///     override the component methods.
        /// </summary>
        public bool ReadNextSection()
        {
            if (this.IsQuote)
            {
                this.ReadQuotedString();
                return false;
            }

            if (this.BeginDashDashComment)
            {
                return this.ReadDashDashComment();
            }

            if (this.BeginSlashStarComment)
            {
                this.ReadSlashStarComment();
                return false;
            }

            return this.ReadNext();
        }

        protected virtual bool ReadDashDashComment()
        {
            this.Splitter.Append(this.Current);
            while (this.Splitter.Next())
            {
                this.Splitter.Append(this.Current);
                if (this.EndOfLine)
                {
                    break;
                }
            }
            //We should be EndOfLine or EndOfScript here.
            this.Splitter.SetParser(new SeparatorLineReader(this.Splitter));
            return false;
        }

        protected virtual void ReadSlashStarComment()
        {
            if (this.ReadSlashStarCommentWithResult())
            {
                this.Splitter.SetParser(new SeparatorLineReader(this.Splitter));
            }
        }

        private bool ReadSlashStarCommentWithResult()
        {
            this.Splitter.Append(this.Current);
            while (this.Splitter.Next())
            {
                if (this.BeginSlashStarComment)
                {
                    this.ReadSlashStarCommentWithResult();
                    continue;
                }
                this.Splitter.Append(this.Current);

                if (this.EndSlashStarComment)
                {
                    return true;
                }
            }
            return false;
        }

        protected virtual void ReadQuotedString()
        {
            this.Splitter.Append(this.Current);
            while (this.Splitter.Next())
            {
                this.Splitter.Append(this.Current);
                if (this.IsQuote)
                {
                    return;
                }
            }
        }

        protected abstract bool ReadNext();

        #region Helper methods and properties

        protected static bool CharEquals(char expected, char actual)
        {
            return char.ToLowerInvariant(expected) == char.ToLowerInvariant(actual);
        }

        protected bool CharEquals(char compare)
        {
            return CharEquals(this.Current, compare);
        }

        protected bool HasNext => this.Splitter.HasNext;

        protected char Peek()
        {
            if (!this.HasNext)
                return char.MinValue;
            return (char) this.Splitter.Peek();
        }

        protected bool WhiteSpace => char.IsWhiteSpace(this.Splitter.Current);

        protected bool EndOfLine => '\n' == this.Splitter.Current;

        protected bool IsQuote => '\'' == this.Splitter.Current;

        protected char Current => this.Splitter.Current;

        protected char LastChar => this.Splitter.LastChar;

        private bool BeginDashDashComment => this.Current == '-' && this.Peek() == '-';

        private bool BeginSlashStarComment => this.Current == '/' && this.Peek() == '*';

        private bool EndSlashStarComment => this.LastChar == '*' && this.Current == '/';

        #endregion
    }
}