﻿/*
Created by Wilhelm Liao on 2015-12-25.

Copyright (c) 2015, Wilhelm Liao
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this
  list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice,
  this list of conditions and the following disclaimer in the documentation
  and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Extensions.Data
{
    [ComVisible(true)]
    public sealed class XXHash32CryptoServiceProvider : XXHash32
    {
        private XXHash.State32 _state;

        public XXHash32CryptoServiceProvider()
            : this(0)
        {

        }
        public XXHash32CryptoServiceProvider(int seed)
        {
            _state = new XXHash.State32();
            unchecked
            {
                _state.seed = (uint)seed;
            }
            Initialize();
        }
        public XXHash32CryptoServiceProvider(uint seed)
            : this(unchecked((int)seed))
        {
        }

        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            XXHash.ErrorCode errorCode = XXHash.InternalUpdateState32(_state, array, ibStart, cbSize);

            if (errorCode != XXHash.ErrorCode.XXH_OK)
                throw new InvalidOperationException();
        }
        protected override byte[] HashFinal()
        {
            uint value = XXHash.InternalDigestState32(_state);
            return BitConverter.GetBytes(value);
        }
        public override void Initialize()
        {
            XXHash.InternalResetState32(_state, _state.seed);
        }
    }
}
