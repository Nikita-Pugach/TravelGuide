<!-- Context: project-intelligence/technical | Priority: critical | Version: 2.2 | Updated: 2026-05-23 -->

# Technical Domain

**Purpose**: Технический стек, архитектурные принципы и стандарты разработки EJ.
**Last Updated**: 2026-05-23

## Quick Reference
- **Update Triggers**: Смена стека | Новые архитектурные решения | Изменение стандартов
- **Audience**: Разработчики, AI-агенты
- **Ключевой принцип**: Простая надёжная система > ультрасовременная архитектура

## Primary Stack
| Layer | Technology | Примечание |
|-------|-----------|------------|
| Frontend | React + Vite + TypeScript + Tailwind CSS | TanStack Table, Recharts |
| Backend | Node.js + TypeScript + Express | Микросервисы, возможен modular monolith |
| Database | PostgreSQL | Единая БД для всех сервисов |
| Auth | JWT (access + refresh) | CAPTCHA, lockout, invite-flow |
| Storage | MinIO (S3-совместимое) | Docker-том |
| Cache/Pub-Sub | Redis | WebSocket, pub-sub |
| Containerization | Docker Compose | Self-hosted, single-tenant |
| Monitoring | JSON-логи, health checks | Graceful degradation |

## Architecture Approach
**Pragmatic Modular** — направление к микросервисам, но без фанатизма.

| Что делаем | Что НЕ делаем |
|-----------|---------------|
| Чёткое разделение сервисов по зонам ответственности | Overengineering ради «красоты» |
| REST API, Redis Pub/Sub для синхронизации | DDD/CQRS/event-driven без реальной необходимости |
| Каждый сервис — отдельное приложение | Чрезмерная декомпозиция |
| Если modular monolith надёжнее — выбираем его | Микросервисы как религия |

**Решение**: см. decisions-log.md «Architecture Approach — Pragmatic Microservices»

## Code Principles
1. **TypeScript everywhere** — strict mode, строгая типизация обязательна
2. **Минимум магии** — никакого неявного поведения
3. **Единые паттерны API** — все эндпоинты следуют одинаковой структуре
4. **Валидация на всех уровнях** — сервер (Zod), клиент (React Hook Form)
5. **Чёткое разделение ответственности** — один модуль = одна зона ответственности
6. **Документирование решений** — каждое архитектурное решение в decisions-log.md
7. **Хорошая DX** — быстрый старт, понятные скрипты, минимум конфигурации
8. **Комментарии на русском** — код комментируем на русском языке для единого контекста команды
9. **Никакого хардкода** — все строки, конфиги, значения через env/константы/конфиг-файлы
10. **Коммиты на английском** — Conventional Commits, стандартный стиль, английский язык

## UX Principles
1. **«Поймёт ли преподаватель без инструкции?»** — главный критерий любого экрана
2. **Работа в стрессе** — интерфейс не наказывает за ошибку, не требует догадок
3. **Минимум действий для задачи** — поставить оценку должно быть быстрее, чем в бумажном журнале
4. **Не перегруженный экран** — одна страница = один сценарий
5. **Мобильная адаптация** — журнал на телефоне должен быть читаем и работаем
6. **Предсказуемое состояние** — никаких «магических» изменений данных

**Ядро системы**: журнал и расписание — максимальная скорость, устойчивость, понятность.

## Quality Standards
- ESLint + Prettier — обязательны, настроены на весь проект
- Code review — обязателен для каждого PR
- Единые conventions — именование, структура файлов, паттерны
- Автотесты — разумно, критичные бизнес-сценарии в первую очередь
- E2E для ключевых flows (выставление оценки, регистрация, просмотр журнала)

## Security Requirements
- Onboarding через invite-коды (10+ символов, случайные токены)
- JWT (access + refresh), ролевая модель
- CAPTCHA на регистрации и входе
- Lockout: 5 неудачных попыток → 15 мин блокировки
- WebSocket auth через Sec-WebSocket-Protocol (не query-параметр)
- Audit logs на уровне учреждения
- File scanning (ClamAV или аналог)
- CSP helmet, security headers
- Rate limiting: 60 подключений/мин на WS, pagination ≤ 100

## DevOps
- Docker Compose — единственная команда для запуска
- Self-hosted — установка на сервер учреждения
- Graceful degradation — отказ одного сервиса не ломает другие
- Non-root контейнеры (UID 1000)
- Graceful shutdown (SIGTERM → 30s timeout)
- Backup/restore скрипты
- Health checks для каждого сервиса

## 📂 Codebase References
- `TECHNICAL_SPECIFICATION.md` — полное ТЗ с архитектурой, ролями, API
- `DESIGN.md` — дизайн-система (цвета, типографика, компоненты)
- `Prototypes/` — текущий прототип frontend (будет заменён)
- `docs/obsidian/` — детальная документация для команды

## Related Files
- `decisions-log.md` — архитектурные решения (см. «Architecture Approach»)
- `business-domain.md` — бизнес-контекст и пользователи
