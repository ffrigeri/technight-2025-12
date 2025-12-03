# Technight 2025-12 Project Requirements Analysis

## Executive Summary

This is a **hackathon project** for NYBBLE GROUP's December 2025 Tech Challenge. The goal is to build an AI-powered, gamified engagement and feedback application for events, meetings, and webinars in **3 hours** using **100% agentic AI development** (no manual coding).

## Project Overview

### Core Objective (Spanish: "Objetivo")
Develop an intelligent, visually attractive web application with **gamification** and **AI integration** for collecting surveys/feedback/voting from events/meetings/webinars, measuring participation and attendance, to be used during all NYBBLE GROUP presentations and integrated into their hub as a social space.

### Key Business Goals
1. **Generate, measure, and obtain engagement** through gamification and social experiences
2. Cover the **complete PRE/DURING/POST** event lifecycle uniquely "the NYBBLE WAY"
3. Associate the web app with calendar meetings to segment by experience
4. Must be **shareable to external participants** (critical for webinars), but dashboard remains private
5. Provide **dynamic, recurring feedback** options that are **fun** and bring **valuable information**
6. Include **intelligent, probing questions** (possibly AI-generated) based on what happened
7. Simulate **Slack communication** for certain interactions with a **unique graphic interface**
8. **Infer data** from other systems (not just ask questions)

## Technical Architecture

### Monorepo Structure
```
technight-2025-12/
├── backend/              # Multiple backend options (choose ONE)
│   ├── node/            # Express + TypeScript + Prisma ORM ⭐ RECOMMENDED
│   ├── python/          # FastAPI + SQLAlchemy ORM
│   ├── springboot/      # Spring Boot + JPA/Hibernate
│   └── dotnet/          # ASP.NET Core + Entity Framework
├── frontend/            # React 19 + Vite 6 + TypeScript
└── Root configuration files
```

### Technology Stack

**Frontend (Mandatory):**
- React 19
- Vite 6
- TypeScript 5.6
- Native fetch API for backend communication
- Port: 5173

**Backend (Choose ONE):**
1. **Node.js** (Pre-configured, Recommended)
   - Express + TypeScript 5.6
   - Prisma ORM
   - Swagger/OpenAPI docs
   - Port: 8080

2. **Python** (Pre-configured)
   - FastAPI + Python 3.12
   - SQLAlchemy ORM
   - Uvicorn server
   - Auto-generated Swagger UI
   - Port: 8080

3. **Java/Spring Boot** (Available)
   - Spring Boot + Java 17+
   - JPA/Hibernate ORM
   - SpringDoc OpenAPI 3
   - Port: 8080

4. **.NET Core** (Available)
   - ASP.NET Core + C#
   - Entity Framework Core
   - Swashbuckle Swagger
   - Port: 8080

**Database:**
- PostgreSQL (one database per team)
- Connection via respective ORM (Prisma/SQLAlchemy/JPA/EF)
- Credentials provided during event

**AI Integration:**
- Gemini API Key (provided during event)
- Documentation: https://ai.google.dev/gemini-api/docs?hl=es-419

## Functional Requirements

### 1. Event Lifecycle Coverage

**PRE-Event Phase:**
- Event setup and configuration
- Associate with calendar meeting
- Pre-event surveys/questions
- Participant registration (internal + external)

**DURING-Event Phase:**
- Real-time feedback collection
- Live polls and voting
- Dynamic, recurring questions
- Engagement tracking
- Gamification mechanics (points, badges, leaderboards)
- Simulated Slack notifications/interactions

**POST-Event Phase:**
- Post-event surveys
- Analytics and insights
- AI-powered concept extraction
- Summary generation

### 2. Data Integration (Simulated Initially)

The system should integrate (or simulate) data from:
- **People Force**: Basic people information, photos, skills
- **Calendar**: Event information
- **Previous Events**: Historical event data
- **Media**: Videos/photos from events (virtual or in-person)
- **Meeting Recordings**: Recording data
- **AI Summaries**: Extracted summaries
- **Attendance**: Meeting attendance tracking
- **Fellow Summary**: Summary data, etc.

### 3. Dashboard & Analytics

**Requirements:**
- Friendly dashboard/summary screen
- Event/meeting statistics
- AI-augmented insights to extract key concepts and metrics
- Visualizations for teams
- Private access (only for organizers)

### 4. Intelligent Questioning

- Generic baseline questions
- **AI-generated probing questions** based on event context
- Dynamic feedback mechanisms
- Recurring question options
- Context-aware surveys

### 5. Gamification Elements

- Point systems
- Badges/achievements
- Leaderboards
- Fun, engaging interactions
- Social features
- Encourages repeated participation

### 6. Shareable Access

- Public links for external participants (webinars)
- No authentication required for participants
- Private dashboard for organizers
- Event-specific instances

## Non-Functional Requirements

### 1. Development Constraints

**Mandatory Rules:**
- ✅ **100% Agentic AI Development** - NO manual coding
- ✅ Use ONE of: **Cursor Agent**, **Gemini CLI**, or **Claude Code**
- ✅ Team acts as "governors of the agents"
- ✅ Fork the base repository: https://github.com/nybblegroup/technight-2025-12
- ✅ Read README.md for project setup

### 2. Timeline (3 Hours Total)

| Phase | Duration | Activities |
|-------|----------|------------|
| KickOff | 15 min | Tech Challenge definition |
| Ideation + Discovery | 45 min | Define scope and planning |
| Execution with AI | ~60 min | AI-driven creation |
| Break | 15-20 min | Prepare presentations |
| Presentations | ~40 min | Demos and voting |

### 3. Working Modes

**PLANNING MODE (📋):**
- Determine tasks when scope is clear
- Use base prompt: `SPEC_PROMPT.md`
- Document scope and planning
- Decide: single prompt vs. separate user stories

**EXECUTION MODE (🚀):**
- Based on planning tasks and discovery
- Provide all necessary context
- AI agent implementation

**PRESENTATION MODE (🎤):**
- **Business representative** presents (part of evaluation!)
- Live product demo
- Showcase functionality

### 4. Technical Constraints

- **Node Backend**: Pre-configured, recommended starting point
- **Python Backend**: Pre-configured, alternative option
- **Frontend**: Common React + Vite for all teams
- **CORS**: Configured for localhost:5173 and localhost:3000
- **No deployment**: Local development only
- **No authentication**: Minimal baseline setup
- **Database**: PostgreSQL with team-specific credentials
- **API Documentation**: All backends expose Swagger UI
- **Port Usage**: Backend on 8080, Frontend on 5173

## Environment & Infrastructure

### GitHub Setup
- Fork base repo: https://github.com/nybblegroup/technight-2025-12
- GitHub Codespaces support included
- DevContainer with Node.js 20 + Python 3.12
- Pre-configured VS Code extensions

### Available Scripts
```bash
npm run dev              # Node backend + Frontend (default)
npm run dev:backend      # Node backend only
npm run dev:python       # Python backend only
npm run dev:frontend     # Frontend only
npm run build            # Production build
```

### API Endpoints (All Backends)
- Health Check: `GET /api/health`
- Swagger UI: `GET /api/swagger`
- OpenAPI Spec: `GET /api/openapi.json`
- Example CRUD endpoints included

## Success Criteria

### Evaluation Factors
1. **Functionality**: Does it work? Does it solve the problem?
2. **User Experience**: Is it visually attractive and engaging?
3. **Gamification**: Are the gamification elements fun and effective?
4. **AI Integration**: Is AI used effectively for insights/questions?
5. **Innovation**: Is it unique and done "the NYBBLE WAY"?
6. **Presentation**: Business representative's demo quality

### Prize
- **Winner** (voted by all participants) gets:
  - Integration into new Nybble Hub as innovation initiative during 2026
  - Access available to all employees
  - Christmas prize (not specified, but something!)

## Support Resources

### Documentation Files
- `README.md` - Comprehensive setup guide for all backends
- `CHALLENGE.md` - Business requirements (Spanish)
- `SPEC_PROMPT.md` - Feature planning template
- `BACKEND_ENDPOINTS.md` - API endpoint documentation
- `AGENTS.md` - Development instructions (symlink to README)

### Support During Event
- **Slack Channel**: `#topgun-technight`
- **Mentors**: Available for technical questions
- **Credentials**: Shared at event start
  - PostgreSQL database credentials
  - Gemini API Key

### Key Advice for Success
1. 💡 **Focus on experience**: Make it fun and useful
2. 🎯 **Define scope well**: Don't try to do everything, prioritize
3. 🤖 **Trust the AI**: Let agents do the heavy lifting
4. 📊 **Visual data**: Good dashboard is worth 1000 words
5. 🎮 **Gamify everything**: Make it addictive and entertaining
6. 🎤 **Prepare the demo**: Presentation counts heavily
7. 👥 **Work as team**: Divide responsibilities
8. ⏰ **Manage time**: 3 hours pass quickly

## Current Project State

This is a **starter template/boilerplate** with:
- ✅ Pre-configured monorepo structure
- ✅ Multiple backend options ready to use
- ✅ React + Vite frontend setup
- ✅ GitHub Codespaces integration
- ✅ Development container configuration
- ✅ All dependencies pre-defined
- ✅ Swagger/OpenAPI documentation setup
- ✅ Basic health check endpoints
- ✅ Example CRUD operations

**Ready for**: Teams to fork and build their gamified event engagement platform with AI agents.

## Key Differentiators (NYBBLE WAY)

1. **Complete Lifecycle**: PRE/DURING/POST coverage
2. **AI-Powered Intelligence**: Smart questions and insights
3. **External Sharing**: Webinar-ready with public access
4. **Gamification Focus**: Make engagement fun and rewarding
5. **Data Integration**: Infer from multiple systems
6. **Social Features**: Slack integration, collaborative elements
7. **Unique UI**: Visually distinctive graphic interface
8. **Value Extraction**: Not just collect data, generate insights

## Technical Approach Recommendations

### Phase 1: Setup (Ideation + Discovery - 45 min)
1. Fork repository
2. Choose backend technology (recommend Node.js or Python)
3. Set up local development environment
4. Define minimal viable feature set
5. Create implementation plan using SPEC_PROMPT.md template
6. Prioritize features for 1-hour execution window

### Phase 2: Execution (~60 min)
1. Database schema design (events, participants, questions, responses, gamification)
2. Backend API endpoints
3. Frontend UI components
4. AI integration (Gemini API)
5. Gamification mechanics
6. Dashboard/analytics view
7. Testing and bug fixes

### Phase 3: Presentation (15-20 min prep + 40 min demos)
1. Prepare demo script
2. Load sample data
3. Test all features
4. Business representative practice
5. Highlight unique "NYBBLE WAY" elements

## Risk Mitigation

**Time Risk**: Only 1 hour for execution
- **Mitigation**: Focus on core features, use pre-built components, trust AI agents

**Integration Risk**: Multiple systems to simulate
- **Mitigation**: Use mock data, focus on UI/UX, demonstrate concept

**Technical Risk**: Unfamiliar with agentic development
- **Mitigation**: Use base prompts, follow SPEC_PROMPT.md, ask mentors

**Scope Risk**: Too many features
- **Mitigation**: Strict prioritization, MVP mindset, defer nice-to-haves

## Conclusion

This is a well-structured hackathon challenge with:
- **Clear business objectives** (engagement, gamification, AI)
- **Flexible technical stack** (4 backend options)
- **Comprehensive starter template** (saves setup time)
- **Innovative constraint** (100% agentic development)
- **Valuable prize** (integration into production hub)

Success depends on:
1. Smart scope definition in 45-minute planning phase
2. Effective AI agent governance during 60-minute execution
3. Strong business presentation with working demo
4. Creating something uniquely "NYBBLE WAY"
