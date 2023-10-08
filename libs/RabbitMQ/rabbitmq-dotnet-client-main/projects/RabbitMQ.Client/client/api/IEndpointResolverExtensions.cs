﻿// This source code is dual-licensed under the Apache License, version
// 2.0, and the Mozilla Public License, version 2.0.
//
// The APL v2.0:
//
//---------------------------------------------------------------------------
//   Copyright (c) 2007-2020 VMware, Inc.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       https://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//---------------------------------------------------------------------------
//
// The MPL v2.0:
//
//---------------------------------------------------------------------------
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
//
//  Copyright (c) 2007-2020 VMware, Inc.  All rights reserved.
//---------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace RabbitMQ.Client
{
    public static class EndpointResolverExtensions
    {
        public static T SelectOne<T>(this IEndpointResolver resolver, Func<AmqpTcpEndpoint, T> selector)
        {
            var t = default(T);
            List<Exception> exceptions = null;
            foreach (AmqpTcpEndpoint ep in resolver.All())
            {
                try
                {
                    t = selector(ep);
                    if (t.Equals(default(T)) == false)
                    {
                        return t;
                    }
                }
                catch (Exception e)
                {
                    exceptions ??= new List<Exception>(1);
                    exceptions.Add(e);
                }
            }

            if (Object.Equals(t, default(T)) && exceptions?.Count > 0)
            {
                throw new AggregateException(exceptions);
            }

            return t;
        }
    }
}
