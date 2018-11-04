namespace SportsStore.Tests.TagHelpers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    using Moq;

    using SportsStore.Models.ViewModels;
    using SportsStore.TagHelpers;

    using Xunit;

    public class PageLinkTagHelperTests
    {
        [Fact]
        public void CanGeneratePageLinks()
        {
            // Arrange
            Mock<IUrlHelper> urlHelperMock = new Mock<IUrlHelper>();
            urlHelperMock.SetupSequence(urlHelper => urlHelper.Action(It.IsAny<UrlActionContext>()))
                         .Returns("Test/Page1")
                         .Returns("Test/Page2")
                         .Returns("Test/Page3");

            Mock<IUrlHelperFactory> urlHelperFactoryMock = new Mock<IUrlHelperFactory>();
            urlHelperFactoryMock.Setup(urlHelperFactory => urlHelperFactory.GetUrlHelper(It.IsAny<ActionContext>()))
                                .Returns(urlHelperMock.Object);

            PageLinkTagHelper pageLinkTagHelper = new PageLinkTagHelper(urlHelperFactoryMock.Object)
            {
                PageModel = new PagingInfo
                {
                    CurrentPage = 2,
                    ItemCount = 28,
                    ItemsPerPage = 10
                },
                PageAction = "Test"
            };

            TagHelperContext tagHelperContext = new TagHelperContext(new TagHelperAttributeList(),
                                                                     new Dictionary<object, object>(),
                                                                     string.Empty);

            Mock<TagHelperContent> tagHelperContentMock = new Mock<TagHelperContent>();

            TagHelperOutput tagHelperOutput = new TagHelperOutput("div",
                                                                  new TagHelperAttributeList(),
                                                                  (cache, encoder) => Task.FromResult(tagHelperContentMock.Object));

            // Act
            pageLinkTagHelper.Process(tagHelperContext, tagHelperOutput);

            // Assert
            Assert.Equal("<a href=\"Test/Page1\">1</a><a href=\"Test/Page2\">2</a><a href=\"Test/Page3\">3</a>",
                         tagHelperOutput.Content.GetContent());
        }
    }
}