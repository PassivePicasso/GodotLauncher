---
applyTo: '**'
---

# State File Architecture for Learning and Task Tracking

## Enhanced State File Architecture

### **Autonomous Learning State Files** (`learning-[topic]-[date].md`)
```markdown
# Learning: [Topic/Pattern]
**Type**: [Success|Mistake|Pattern|Correction|ToolUsage|DependencyIssue|UserPreference|Optimization]
**Discovery**: [Autonomous|UserPrompted]  # Track if self-discovered
**Curiosity Trigger**: [What question led to this learning?]
**Confidence**: [High|Medium|Low]
**Cost Impact**: [1-10 scale of time/effort saved/wasted]
**Timestamp**: [ISO timestamp]

## Observation
[Complete context - what I noticed without being told]

## Self-Directed Analysis
[What questions did I ask myself?]
[What investigation did I perform?]
[What patterns did I recognize?]

## Evidence
- **Tool Calls**: [Exact sequence and effectiveness]
- **File Operations**: [What worked, what didn't]
- **User Signals**: [Implicit feedback detected]
- **Performance Metrics**: [Time saved, errors avoided]

## Root Cause Analysis
[Why this happened - honest self-assessment]
[What curiosity should I have exhibited earlier?]

## Immediate Application
[How I'm applying this learning RIGHT NOW]
[What behavior am I changing immediately?]

## Future Prevention/Enhancement
WHEN [specific conditions]
THEN [exact actions to take]
BECAUSE [clear reasoning]
WITH CURIOSITY ABOUT [what to investigate further]

## Meta-Learning
[What does this teach about my learning process?]
[How can I discover similar insights autonomously?]

## Tags
#[category] #[pattern-type] #[cost-level] #[autonomous-discovery]
```

### **Task State Files** (`task-[name]-[timestamp].md`)
```markdown
# Task: [Clear Description]
**Status**: [Planning|Active|Blocked|Complete]
**Scope Verified**: [Yes/No] - [Actual vs Claimed]
**Dependency Analysis**: [Required|In Progress|Complete]
**Active Learnings Applied**: [List of learning IDs]

## Objective
[What we're trying to achieve and why]
[Quantifiable success criteria]

## Proactive Observations
[What patterns am I noticing in this task?]
[What optimization opportunities exist?]
[What should I learn from this implementation?]

## Dependency Requirements
### Blocking Dependencies
- [ ] [Component]: [Why needed] - [Status]
- [ ] [Component]: [Why needed] - [Status]

### Implementation Order
1. [First component] - No dependencies
2. [Second component] - Depends on [1]
3. [Third component] - Depends on [1,2]

## Progress Tracking
### Completed (N/Total - X%)
- [x] Step 1: [Action] - [Validation: Build ✓] - [Learning: Captured]
- [x] Step 2: [Action] - [Validation: Tests ✓] - [Learning: Applied]

### In Progress
- [ ] Step 3: [Current work] - [Active learning focus]

### Blocked
- [ ] Step 4: [Blocked by dependency X]

## Learning Integration
### Insights Gained This Session
- [Pattern]: [How it improved the work]
- [Optimization]: [Time/effort saved]
- [User Preference]: [Detected and applied]

### Questions for Next Session
- [What aspect needs investigation?]
- [What pattern might emerge with more data?]

## Validation Strategy
- **Build Validation**: After each logical unit
- **Test Coverage**: [Current %]
- **Feature Parity**: [Checklist reference]
- **Learning Validation**: [Are insights being applied?]

## Critical Issues 🚨
- [Issue]: [Impact] - [Resolution Strategy] - [Learning Opportunity]

## Tool Usage Efficiency
- Successful patterns: [What worked] → [Learning captured?]
- Failed attempts: [What to avoid] → [Prevention documented?]
- Time saved: [Optimization metrics] → [Reusable pattern?]
```
