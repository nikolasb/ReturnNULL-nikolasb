@echo off
IF "%SITE_FLAVOR%" == "helloworld" (
  deploy.ClassProject.cmd
) ELSE (
  IF "%SITE_FLAVOR%" == "eachvoice" (
    deploy.EachVoice.cmd
  ) ELSE (
    echo You have to set SITE_FLAVOR setting to either "helloworld" or "eachvoice"
    exit /b 1
  )
)