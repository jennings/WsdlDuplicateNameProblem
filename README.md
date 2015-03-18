What is this?
=============

This is a [Short, Self-Contained, Correct Example](http://sscce.org) for this
Stack Overflow Question: [WCF generated proxy throws InvalidOperationException due to multiple types with same name in WSDL][so]

[so]: http://stackoverflow.com/questions/29132014/wcf-generated-proxy-throws-invalidoperationexception-during-serialization


Question text
-------------

I'm using Visual Studio 2013 to generate a WCF service proxy from [this WSDL
file](http://services.hotschedules.com/api/services/SalesService?wsdl).
However, as soon as I try to call the `setSalesItemsV3` method, WCF throws an
`InvalidOperationException` from deep in `System.Xml.dll`.

This sample project demonstrates the problem:
https://github.com/jennings/WsdlDuplicateNameProblem

This is the inner exception:

> **Message:** The top XML element 'start' from namespace '' references distinct types WsdlDuplicateName.SalesItemService.hsSimpleDate and System.DateTime. Use XML attributes to specify another XML name or namespace for the element or types.

I'm no expert at reading WSDL, but I've looked at it and the only sections that
reference the name "start" are a few `<wsdl:part>` elements with
`name="start"`:

    <wsdl:message name="setSalesItems">
      <wsdl:part name="start" type="xsd:dateTime"></wsdl:part>
    </wsdl:message>

    <wsdl:message name="setSalesItemsV3">
      <wsdl:part name="start" type="tns:hsSimpleDate"></wsdl:part>
    </wsdl:message>

But, the parts are in completely different messages, so I don't see why there
should be any confusion. I've run the WSDL file through several online WSDL
validators and they seem to be okay with it.

Below is the only code in the project necessary to reproduce the problem
(besides the generated proxy).

    class Program
    {
        static void Main(string[] args)
        {
            SalesServiceClient client = new SalesServiceClient();
            var date = ToSimpleDate(new DateTime());

            // throws InvalidOperationException
            // Message == "There was an error reflecting 'start'."
            client.setSalesItemsV3(1, 1, null, date, date);
        }

        static hsSimpleDate ToSimpleDate(DateTime time)
        {
            return new hsSimpleDate
            {
                year = time.Year,
                month = time.Month,
                day = time.Day,
            };
        }
    }
