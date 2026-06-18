<!-- Context: project-intelligence/decisions | Priority: high | Version: 1.0 | Updated: 2025-01-12 -->

# Decisions Log

> Record major architectural and business decisions with full context. This prevents "why was this done?" debates.

## Quick Reference

- **Purpose**: Document decisions so future team members understand context
- **Format**: Each decision as a separate entry
- **Status**: Decided | Pending | Under Review | Deprecated

## Decision Template

```markdown
## [Decision Title]

**Date**: YYYY-MM-DD
**Status**: [Decided/Pending/Under Review/Deprecated]
**Owner**: [Who owns this decision]

### Context
[What situation prompted this decision? What was the problem or opportunity?]

### Decision
[What was decided? Be specific about the choice made.]

### Rationale
[Why this decision? What were the alternatives and why were they rejected?]

### Alternatives Considered
| Alternative | Pros | Cons | Why Rejected? |
|-------------|------|------|---------------|
| [Alt 1] | [Pros] | [Cons] | [Why not chosen] |
| [Alt 2] | [Pros] | [Cons] | [Why not chosen] |

### Impact
**Positive**: [What this enables or improves]
**Negative**: [What trade-offs or limitations this creates]
**Risk**: [What could go wrong]

### Related
- [Links to related decisions, PRs, issues, or documentation]
```

---

## Decision: [Title]

**Date**: YYYY-MM-DD
**Status**: [Status]
**Owner**: [Owner]

### Context
[What was happening? Why did we need to decide?]

### Decision
[What we decided]

### Rationale
[Why this was the right choice]

### Alternatives Considered
| Alternative | Pros | Cons | Why Rejected? |
|-------------|------|------|---------------|
| [Option A] | [Good things] | [Bad things] | [Reason] |
| [Option B] | [Good things] | [Bad things] | [Reason] |

### Impact
- **Positive**: [What we gain]
- **Negative**: [What we trade off]
- **Risk**: [What to watch for]

### Related
- [Link to PR #000]
- [Link to issue #000]
- [Link to documentation]

---

## Decision: [Title]

**Date**: YYYY-MM-DD
**Status**: [Status]
**Owner**: [Owner]

### Context
[What was happening?]

### Decision
[What we decided]

### Rationale
[Why this was right]

### Alternatives Considered
| Alternative | Pros | Cons | Why Rejected? |
|-------------|------|------|---------------|
| [Option A] | [Good things] | [Bad things] | [Reason] |

### Impact
- **Positive**: [What we gain]
- **Negative**: [What we trade off]

### Related
- [Link]

---

## Decision: Architecture Approach — Pragmatic Microservices

**Date**: 2026-05-23
**Status**: Decided
**Owner**: Команда EJ

### Context
Проект EJ — self-hosted электронный журнал для учебных заведений Беларуси. Текущий прототип на React + Vite. Планируется переход на серверную архитектуру. Команда — 4 разработчика, сроки не ограничены.

### Decision
Принимаем **pragmatic modular approach**:
- Сервисы выделяются по зонам ответственности (Auth, Journal, Schedule, Lab, File)
- Gateway (BFF) как единая точка входа
- Микросервисы — направление, но НЕ догма
- Если modular monolith на старте надёжнее — выбираем его

### Rationale
- UX и надёжность важнее архитектурной «красоты»
- Команда небольшая — чрезмерная декомпозиция замедлит разработку
- Self-hosted single-tenant — нагрузка не требует горизонтального масштабирования
- Микросервисы оправданы, когда есть реальные независимые домены (у нас они есть: авторизация, журнал, файлы)
- Но без фанатизма — если сервис слишком мал, объединяем

### Alternatives Considered
| Alternative | Pros | Cons | Why Rejected? |
|-------------|------|------|---------------|
| Modular Monolith | Проще деплой, меньше сетевых задержек, легче рефакторинг | Жёсткая связанность, сложнее параллельная разработка | Не rejected — может быть выбран, если анализ покажет |
| Full Microservices | Чёткая изоляция, параллельная разработка | Сложность деплоя, сетевая задержка, overhead | Принят как направление |
| Serverless | Масштабирование, меньше DevOps | Vendor lock-in, холодный старт | Противоречит self-hosted модели |

### Impact
- **Positive**: Гибкость выбора, команда может принимать решения по ситуации
- **Negative**: Неопределённость — нужно постоянно переоценивать границы сервисов
- **Risk**: «Middle ground» — ни монолит, ни микросервисы, а худшее из обоих

### Related
- `technical-domain.md` — архитектурный подход и стек

---

## Deprecated Decisions

Decisions that were later overturned (for historical context):

| Decision | Date | Replaced By | Why |
|----------|------|-------------|-----|
| [Old decision] | [Date] | [New decision] | [Reason] |

## Onboarding Checklist

- [ ] Understand the philosophy behind major architectural choices
- [ ] Know why certain technologies were chosen over alternatives
- [ ] Understand trade-offs that were made
- [ ] Know where to find decision context when questions arise
- [ ] Understand what decisions are pending and why

## Related Files

- `technical-domain.md` - Technical implementation affected by these decisions
- `business-tech-bridge.md` - How decisions connect business and technical
- `living-notes.md` - Current open questions that may become decisions
