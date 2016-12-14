﻿// Copyright © 2013, 2016 Oracle and/or its affiliates. All rights reserved.
//
// MySQL Connector/NET is licensed under the terms of the GPLv2
// <http://www.gnu.org/licenses/old-licenses/gpl-2.0.html>, like most 
// MySQL Connectors. There are special exceptions to the terms and 
// conditions of the GPLv2 as it is applied to this software, see the 
// FLOSS License Exception
// <http://www.mysql.com/about/legal/licensing/foss-exception.html>.
//
// This program is free software; you can redistribute it and/or modify 
// it under the terms of the GNU General Public License as published 
// by the Free Software Foundation; version 2 of the License.
//
// This program is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
// or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License 
// for more details.
//
// You should have received a copy of the GNU General Public License along 
// with this program; if not, write to the Free Software Foundation, Inc., 
// 51 Franklin St, Fifth Floor, Boston, MA 02110-1301  USA

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections.Specialized;

namespace MySql.Data.MySqlClient.Tests
{
#if !NETCORE10
    public class GenericListener : TraceListener
#else
    public class GenericListener
#endif
    {
    List<string> strings;
    StringBuilder partial;

    public GenericListener()
    {
      strings = new List<string>();
      partial = new StringBuilder();
    }

    public List<string> Strings
    {
      get { return strings; }
    }

    public int Find(string sToFind)
    {
      int count = 0;
      foreach (string s in strings)
        if (s.IndexOf(sToFind) != -1)
          count++;
      return count;
    }

    public void Clear()
    {
      partial.Remove(0, partial.Length);
      strings.Clear();
    }

#if !NETCORE10
     public override void Write(string message)
#else
     public void Write(string message)
#endif
    {
            partial.Append(message);
    }

#if !NETCORE10
    public override void WriteLine(string message)
#else
    public void WriteLine(string message)
#endif
    {
      Write(message);
      strings.Add(partial.ToString());
      partial.Remove(0, partial.Length);
    }

    public int CountLinesContaining(string text)
    {
      int count = 0;
      foreach (string s in strings)
        if (s.Contains(text)) count++;
      return count;
    }
  }
}
