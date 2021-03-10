using NUnit.Framework;
using SentimentAnalytics.Models;
using Shouldly;
using System;

namespace SentimentAnalytics.Tests
{
    [TestFixture]
    class DocumentTests
    {
        [Test]
        public void DocumentConstructor_ShouldThrowException_IfEmptyStringProvided()
        {
            Should.Throw<InvalidOperationException>(() =>
            {
                string emptyString = "";
                var document = new Document(emptyString);
            }, "Document text cannot be empty");
        }

        [Test]
        public void DocumentConstructor_ShouldThrowException_IfNullStringProvided()
        {
            Should.Throw<InvalidOperationException>(() =>
            {
                string emptyString = null;
                var document = new Document(emptyString);
            }, "Document text cannot be empty");
        }

        [Test]
        public void DocumentConstructor_ShouldThrowException_IfTextProviderIsOver5120Characters()
        {
            Should.Throw<InvalidOperationException>(() =>
            {
                string longString = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc cursus ipsum at magna molestie faucibus. Sed nec turpis eleifend, ultricies lacus sed, tempus ante. In placerat maximus gravida. Integer fermentum imperdiet accumsan. Donec condimentum quam orci, quis luctus odio viverra eu. Ut egestas, tortor non scelerisque vulputate, velit est mattis leo, vitae pharetra velit ex id risus. Aenean dictum felis facilisis turpis commodo lobortis.

Aliquam lacinia, tellus id pellentesque rutrum, dolor sem dignissim lacus, vitae pellentesque massa velit eu magna. Donec a ipsum consectetur, lacinia elit vitae, fermentum nulla. Suspendisse quis eros eu risus tincidunt congue eget luctus tellus. Etiam viverra purus at suscipit finibus. Fusce congue imperdiet nisi rutrum lacinia. Mauris eget hendrerit est, facilisis tincidunt nunc. Sed eu viverra dolor. Etiam maximus ipsum sit amet libero malesuada, eu aliquam purus iaculis.

Praesent vulputate molestie nisl. Praesent sed euismod nisl. Duis bibendum viverra odio, non placerat lorem aliquam et. Phasellus sed auctor sapien. Donec ullamcorper lectus sed molestie porta. Etiam vestibulum turpis sed metus pellentesque, id euismod velit cursus. Phasellus in efficitur arcu. Nullam pulvinar sodales ultricies. Proin dictum augue eget neque vulputate, in luctus tortor scelerisque. Sed elementum velit quis venenatis pellentesque.

Cras vitae porttitor purus. Duis aliquet feugiat finibus. Aenean at volutpat augue, non euismod eros. Quisque nec lacus nec massa gravida vehicula. In eget condimentum urna. In sagittis vehicula eros et faucibus. Aenean a mi ipsum. Cras sed risus elementum, scelerisque velit eget, eleifend dui. Donec vestibulum mi id velit convallis, ut tincidunt ligula dapibus. Nullam scelerisque lobortis justo ut scelerisque. Donec sodales a nisi vitae facilisis. Donec mattis elit vel mollis pretium. Proin fermentum consequat dolor, vitae volutpat libero suscipit accumsan. Donec pulvinar nunc ut neque mattis aliquet. Ut ut elit sit amet mauris dapibus euismod.

Duis malesuada neque dolor. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Maecenas a imperdiet ante, vitae malesuada est. Sed lectus sapien, luctus in ornare vel, vehicula condimentum nibh. Aliquam quis fermentum urna. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Cras non velit placerat, ultrices arcu ut, molestie sapien. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum sed semper ex. Vestibulum sit amet mattis dui, ut aliquet eros. Aliquam accumsan tincidunt nisl nec ultrices. Duis feugiat suscipit quam, in elementum massa congue finibus. Nam sed hendrerit leo. Quisque pulvinar, libero sed egestas porta, ante risus convallis risus, in vulputate leo quam nec arcu. Aenean placerat interdum odio vel varius. Aenean aliquam tellus quis ex bibendum, ac varius tellus vestibulum.

Suspendisse a ante eu nisl maximus pretium. Duis sollicitudin arcu enim, id bibendum massa finibus a. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nunc scelerisque facilisis lectus, sed auctor mi facilisis non. Suspendisse potenti. Sed sodales volutpat metus, quis auctor leo. Nam euismod turpis a est ultrices, at sagittis leo facilisis. Nunc placerat vel quam sit amet vehicula. Nulla eleifend dignissim sem consectetur condimentum. Quisque posuere dapibus nulla varius tincidunt. Aliquam in scelerisque orci.

Morbi facilisis sit amet eros nec ultrices. Duis eleifend ipsum at massa sollicitudin tempor. Suspendisse potenti. Integer ipsum tellus, vestibulum in magna ut, maximus lobortis sem. Nam sed mi non felis sodales feugiat. Sed placerat vestibulum mattis. Curabitur nec mattis arcu. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Donec vehicula nisi nec dui maximus, vitae laoreet turpis viverra. Nulla tortor enim, facilisis non posuere eu, semper et dui. Sed nibh ante, condimentum nec lectus sed, porta accumsan nibh. Curabitur quis mauris lectus. Quisque et purus a ligula feugiat consectetur. Integer facilisis sem metus, quis dignissim nisi tempus condimentum. Ut rutrum fermentum enim, a suscipit mauris pulvinar in.

Pellentesque quis massa ipsum. Sed quis ligula diam. Donec eget convallis nisl, id pellentesque lorem. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Vivamus vel ipsum nec metus sodales tempor. Vestibulum at metus orci. Praesent eu massa tempus lacus tempus tincidunt id non ex. In eget molestie est, aliquam malesuada ipsum. Nulla vitae pulvinar sem, ut convallis massa. Vestibulum efficitur nibh ac felis hendrerit, sit amet luctus risus varius. Nam aliquet orci in porta tincidunt. Duis faucibus elementum nunc, in volutpat est dictum vehicula. Nulla facilisi.

Cras vitae laoreet tortor. Integer vitae lectus vel nibh volutpat vestibulum sed quis massa. Donec porttitor auctor risus, ut tristique turpis posuere ullamcorper. Suspendisse sollicitudin sit amet quis.";
                var document = new Document(longString);
            }, "Text exceeds the 5,120 limit of the analytics API");
        }

        [Test]
        public void DocumentConstructor_ShouldNotThrowException_IfStringUnderLimitLengthProvided()
        {
            Should.NotThrow(() =>
            {
                string emptyString = "This is a valid string";
                var document = new Document(emptyString);
            });
        }
    }
}
