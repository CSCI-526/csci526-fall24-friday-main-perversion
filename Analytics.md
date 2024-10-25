# Analytics
Data collection and visualization pipeline

## Methodology Used: 
### Data Collection:

Added a timer inside our game to keep track of time spent to complete level

```
public float timer;
void Start()
{
    timer = 0.0f;
}

void Update()
{
    timer += Time.deltaTime;

    Vector3 currentPosition = transform.position;
    Vector3 targetPosition = targetObject.transform.position;
    float distance = Vector3.Distance(currentPosition, targetPosition);

    if (distance<=0.25){
        endText.SetText("Success!" + "\n\n" + "Time Spent:" + '\n' + timer.ToString("0.00"));
        Time.timeScale = 0; 
    }
}
```

### Data Visualization:
1. Users could see the time spent for completing this level after game success.

! [TimeSpentAnalytics.png](TimeSpentAnalytics.png)
   
2. Pass the data into RStudio to generate bar plots to see the trend.

```
> level = c("Level 1", "Level 2", "Level 3", "Level 4")
> time = c(8,15,21,62)
> barplot(time, xlab = "Level #", ylab = "Time (s)",names.arg = level)
```
