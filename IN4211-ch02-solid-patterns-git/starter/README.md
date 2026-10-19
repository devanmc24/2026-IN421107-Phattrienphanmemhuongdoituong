# Starter Ch02 — Refactoring EduHub với SOLID và Design Patterns

Bài thực hành Ch02 refactor EduHub theo SOLID và các Design Pattern. Bản triển khai nằm trong `Ch02.Refactor`; phân tích SOLID nằm ở `Ch02.Refactor/docs/solid-analysis.md`.

Ứng dụng tách service, repository, logger và notification qua interface; áp dụng Factory, Decorator, Strategy và Observer. Các quy tắc tên CLB, sức chứa, đăng ký và check-in được đặt trong domain. Unit test bao gồm domain và các pattern.

## Lệnh

```bash
dotnet build Ch02.Refactor.sln
dotnet test Ch02.Refactor.sln
dotnet run --project Ch02.Refactor
```

## Nộp bài

Nếu làm trong repository Git của lớp, nộp trên nhánh `feature/solid-refactor`, commit theo Conventional Commits, rebase lên `main` rồi mở Pull Request (mô tả: what, why, how to test).
