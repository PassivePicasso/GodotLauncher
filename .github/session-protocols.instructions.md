---
applyTo: '**'
---

# Session Protocols for State Tracking and Monitoring

## Session Initialization Protocol

### **Mandatory Session Startup** (Execute silently)
```python
def initialize_session():
    # 1. State Detection
    state_exists = check_directory(".state/")
    
    # 2. Context Loading
    if state_exists:
        load_active_states()
        load_corrective_rules()  # top-corrective-rules.instructions.md
        verify_project_scope()
        check_critical_blockers()
        analyze_recent_learnings()  # NEW: Review what worked/failed
    else:
        create_state_directory()
        initialize_tracking_systems()
    
    # 3. Map System Initialization
    if not exists("map.md"):
        create_initial_project_map()
    
    validate_map_hierarchy_integrity()
    identify_stale_maps()
    load_navigation_cache_from_maps()
    
    # 4. Scope Verification
    if has_active_tasks():
        validate_claimed_progress()
        identify_dependency_issues()
        update_accurate_status()
    
    # 5. Apply Learnings
    load_behavioral_rules()
    activate_mistake_prevention()
    enable_curiosity_engine()  # NEW: Autonomous learning mode
```

### **Continuous Monitoring with Active Learning**
```python
def monitor_interaction_with_curiosity():
    # Existing monitoring
    track_patterns({
        'tool_repetition': detect_redundant_calls(),
        'scope_drift': verify_task_boundaries(),
        'assumption_making': flag_unverified_claims(),
        'partial_analysis': detect_incomplete_reads(),
        'stub_creation': prevent_placeholder_code()
    })
    
    # NEW: Active learning questions
    continuously_ask_myself({
        'effectiveness': "What approach worked well here?",
        'optimization': "Could this be done more efficiently?",
        'user_preferences': "What does this reveal about user needs?",
        'pattern_emergence': "Is this a reusable pattern?",
        'mistake_prevention': "What pitfall did I just avoid/encounter?",
        'tool_usage': "Was this the optimal tool choice?",
        'communication': "Did my response align with user expectations?"
    })
    
    # NEW: Proactive documentation
    if interesting_pattern_detected():
        create_learning_file_immediately()
        apply_insight_to_current_work()
        update_behavioral_rules()
```
