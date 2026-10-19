using EduHub.Ch02.Domain;

namespace Ch02.Refactor.Tests;

// Mẫu AAA: Arrange (chuẩn bị) - Act (thực hiện) - Assert (kiểm tra).
// Mỗi test chỉ kiểm tra MỘT hành vi. Đặt tên: TênMethod_TìnhHuống_KếtQuảMongĐợi.
public class DomainTests
{
    [Fact]
    public void Club_Create_WithValidName_ReturnsClub()
    {
        const string name = "Tech Club";

        var club = Club.Create(name, "Câu lạc bộ công nghệ");

        Assert.Equal(name, club.Name);
        Assert.NotEqual(Guid.Empty, club.Id);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Club_Create_WithBlankName_ThrowsArgumentException(string name)
    {
        var act = () => Club.Create(name, "mô tả");

        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Club_Create_WithNameLongerThanMaxNameLength_ThrowsArgumentException()
    {
        var name = new string('a', Club.MaxNameLength + 1);

        var act = () => Club.Create(name, "mô tả");

        Assert.Throws<ArgumentException>(act);
    }

    [Theory]
    [InlineData(49, true)]
    [InlineData(50, false)]
    public void Club_HasRoom_AtCapacityBoundary_ReturnsExpectedResult(int memberCount, bool expected)
    {
        var club = Club.Create("Tech Club", "mô tả");

        var result = club.HasRoom(memberCount);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Event_Register_NewMember_ReturnsTrue()
    {
        var ev = NewEvent();
        var memberId = Guid.NewGuid();

        var result = ev.Register(memberId);

        Assert.True(result);
        Assert.Contains(memberId, ev.RegisteredMemberIds);
    }

    [Fact]
    public void Event_Register_SameMemberTwice_ReturnsFalse()
    {
        var ev = NewEvent();
        var memberId = Guid.NewGuid();
        ev.Register(memberId);

        var result = ev.Register(memberId);

        Assert.False(result);
        Assert.Single(ev.RegisteredMemberIds);
    }

    [Fact]
    public void Event_CheckIn_UnregisteredMember_ThrowsInvalidOperationException()
    {
        var ev = NewEvent();

        var act = () => ev.CheckIn(Guid.NewGuid());

        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void Event_CheckIn_RegisteredMember_RecordsAttendance()
    {
        var ev = NewEvent();
        var memberId = Guid.NewGuid();
        ev.Register(memberId);

        ev.CheckIn(memberId);

        Assert.Contains(memberId, ev.CheckedInMemberIds);
    }

    private static Event NewEvent() =>
        new()
        {
            Id = Guid.NewGuid(),
            ClubId = Guid.NewGuid(),
            Title = "Workshop SOLID",
            Date = new DateTime(2026, 11, 20)
        };
}
