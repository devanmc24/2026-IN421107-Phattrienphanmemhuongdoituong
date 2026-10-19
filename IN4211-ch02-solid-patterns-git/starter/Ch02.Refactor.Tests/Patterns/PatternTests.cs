using EduHub.Ch02.Domain;
using EduHub.Ch02.Logging;
using EduHub.Ch02.Notifications;
using EduHub.Ch02.Patterns.Observers;
using EduHub.Ch02.Patterns.Strategies;
using EduHub.Ch02.Repositories;
using EduHub.Ch02.Services;

namespace Ch02.Refactor.Tests;

public class PatternTests
{
    private sealed class FakeLogger : IAppLogger
    {
        public List<string> Messages { get; } = new();
        public void Info(string message) => Messages.Add(message);
    }

    private sealed class SpyObserver : IEventObserver
    {
        public int Calls { get; private set; }
        public void OnEventCreated(Event ev, Club club) => Calls++;
    }

    [Theory]
    [InlineData(NotificationChannel.Email, typeof(EmailNotificationService))]
    [InlineData(NotificationChannel.Sms, typeof(SmsNotificationService))]
    [InlineData(NotificationChannel.Slack, typeof(SlackNotificationService))]
    public void Factory_Create_ReturnsServiceForChannel(NotificationChannel channel, Type expectedType)
    {
        var factory = new NotificationServiceFactory();

        var service = factory.Create(channel);

        Assert.IsType(expectedType, service);
    }

    [Fact]
    public void Factory_Create_UnsupportedChannel_ThrowsArgumentOutOfRangeException()
    {
        var factory = new NotificationServiceFactory();

        var act = () => factory.Create((NotificationChannel)99);

        Assert.Throws<ArgumentOutOfRangeException>(act);
    }

    [Fact]
    public void Decorator_Add_LogsAndStoresClub()
    {
        var logger = new FakeLogger();
        var inner = new InMemoryClubRepository();
        IClubRepository repository = new LoggingClubRepository(inner, logger);
        var club = Club.Create("Tech Club", "mô tả");

        repository.Add(club);

        Assert.Contains(logger.Messages, message => message.Contains("Tech Club"));
        Assert.Same(club, inner.GetById(club.Id));
    }

    [Fact]
    public void Strategy_RendersSameStatsInConsoleAndCsvFormats()
    {
        var stats = new List<ClubStat> { new("Tech Club", 2, 1, 2, 1) };

        var console = new ConsoleReportStrategy().Render(stats);
        var csv = new CsvReportStrategy().Render(stats);

        Assert.StartsWith("Club: Tech Club", console);
        Assert.StartsWith("Club,Members", csv);
        Assert.Contains("\"Tech Club\",2,1,2,1", csv);
    }

    [Fact]
    public void Observer_CreateEvent_NotifiesEverySubscriber()
    {
        var clubRepository = new InMemoryClubRepository();
        var club = Club.Create("Tech Club", "mô tả");
        clubRepository.Add(club);
        var service = new EventService(
            clubRepository,
            new InMemoryMemberRepository(),
            new InMemoryEventRepository());
        var firstObserver = new SpyObserver();
        var secondObserver = new SpyObserver();
        service.Subscribe(firstObserver);
        service.Subscribe(secondObserver);

        service.CreateEvent(club.Id, "Workshop", new DateTime(2026, 11, 20));

        Assert.Equal(1, firstObserver.Calls);
        Assert.Equal(1, secondObserver.Calls);
    }

    [Fact]
    public void Observer_Unsubscribe_StopsNotifications()
    {
        var clubRepository = new InMemoryClubRepository();
        var club = Club.Create("Tech Club", "mô tả");
        clubRepository.Add(club);
        var service = new EventService(
            clubRepository,
            new InMemoryMemberRepository(),
            new InMemoryEventRepository());
        var observer = new SpyObserver();
        service.Subscribe(observer);
        service.Unsubscribe(observer);

        service.CreateEvent(club.Id, "Workshop", new DateTime(2026, 11, 20));

        Assert.Equal(0, observer.Calls);
    }
}
