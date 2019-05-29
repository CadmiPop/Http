using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.ExceptionServices;
using System.Text;
using JSONClasses;

namespace Http
{
    public class RequestURI : IPattern
    {
        private IPattern pattern;

        public RequestURI()
        {
            var alpha = new Choice(new Range('a', 'z'), new Range('A', 'Z'));
            var digit = new Range('0','9');

            var scheme = new Sequance(alpha,
                new Choice(alpha,
                    digit,
                    new Any("+-.")));


            var hex = new Choice(digit, new Range('A', 'F'), new Range('a', 'f'));
            var escaped = new Sequance(new Character('%'), hex, hex);
            var alphanum = new Choice(alpha,digit);
            var mark = new Any("-_.!~*'()");
            var unreserved = new Choice(alphanum, mark);
            var userinfo = new Many(new Choice(unreserved, escaped, new Any(";:&=+$,")));
            var IPv4address = new Many(new Many(digit,1,3),4,4);
            var domainlabel = new Choice(alphanum, new Sequance(alphanum, new Many(new Choice(alphanum, new Character('-'))),alphanum));
            var toplabel = new Choice(alpha, new Sequance(alpha, new Many(new Choice(alphanum, new Character('-'))),alphanum));
            var hostname = new Sequance(new Many
                (new Sequance(domainlabel,new Character('.'))), toplabel, new Optional(new Character('.')));
            var host = new Choice(hostname, IPv4address);
            var port = new Many(digit);
            var hostport = new Sequance(host,new Optional(new Sequance(new Character(';'), port)));

            var server = new Optional(
                new Sequance(new Optional(new Sequance(userinfo, new Character('@'))),
                    hostport));

            var reg_name = new Many(new Choice(unreserved,escaped,new Any("$,;:@&=+")));

            var authority = new Choice(server, reg_name);

            var pchar = new Choice(unreserved,escaped,new Any(":@&=+$,"));
            var param = new Many(pchar);
            var segment = new Sequance(new Many(pchar),new Many(new Sequance(new Character(';'),param)));
            var path_segments = new Sequance(segment, new Many(new Sequance(new Character('/'),segment)));
            var abs_path = new Sequance(new Character('/'), path_segments);
            var reserved = new Any(";/?:@&=+$,");
            var uric = new Choice(reserved,unreserved,escaped);
            var uric_no_slash = new Choice(unreserved,escaped, new Any(";?:@&=+$,"));
            var opaque_part = new Sequance(uric_no_slash, new Many(uric));

            var path = new Optional(new Choice(abs_path, opaque_part));
            var query = new Many(uric);
            var fragment = new Many(uric);

            var rel_segment = new OneOrMore(new Choice(unreserved,escaped,new Any(":@&=+$,")));
            var rel_path = new Sequance(rel_segment, new Optional(abs_path));
            var net_path = new Sequance(new Text("//"),authority, new Optional(abs_path));
            var hier_part = new Sequance(new Choice(net_path, abs_path),new Optional(new Sequance(new Character('?'),query)));

            var relativeURI = new Sequance(new Choice(net_path, abs_path, rel_path), new Optional(new Sequance(new Character('?'),query)));
            var absoluteURI = new Sequance(scheme, new Character(':'), new Choice(hier_part, opaque_part));
            this.pattern = new Sequance(new Optional(new Choice(absoluteURI, relativeURI)), 
                new Optional(new Sequance(new Character('#'),fragment)));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
