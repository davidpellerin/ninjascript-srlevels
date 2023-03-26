# ninjascript-srlevels

simple plugin to add Support and Resistance Levels to Ninja Trader 8 charts.

## screenshots

![image](https://user-images.githubusercontent.com/309969/227748626-25334c2c-e9b1-4c62-af20-21115b571f18.png)

![image](https://user-images.githubusercontent.com/309969/227748603-efb3d76a-e3fa-4af9-bfee-46cec8b47488.png)

## properties

SR format is comma separated list of key/value pairs:

```
R5=$4019,R4=$4004,R3=$3993,R2=$3983+,R1=$3958,S1=$3940+,S2=$3931,S3=$3918,S4=$3900+,S5=$3891
```

Make sure that your prices do not contain commas

If you add a + symbol to the price it will draw the line solid (to indicate that it is a stronger level)

