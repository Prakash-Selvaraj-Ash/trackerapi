@echo off
cd..
cd BusTrackerApi
color 2
echo.
echo.
echo ==============================================================
echo Remove bin Directory
echo ==============================================================
echo.
echo.
if exist "bin\" rmdir /q  /s "bin"
echo Removed bin folder is done

echo.
echo.
echo ==============================================================
echo Build api solution 
echo ==============================================================
echo.
echo.
dotnet publish -c Release

echo.
echo.
echo ==============================================================
echo Copy Docker file to publich folder
echo ==============================================================
echo.
echo.
copy Dockerfile  .\bin\Release\netcoreapp2.2\publish

echo.
echo.
echo ==============================================================
echo Docker build new image
echo ==============================================================
echo.
echo.
docker build -t bustrackerapi ./bin/release/netcoreapp2.2/publish

echo.
echo.
echo ==============================================================
echo Docker login to Heroku
echo ==============================================================
echo.
echo.
docker login --username=emteorg@gmail.com --password=6a1b9773-915b-4328-81bb-bf401c09d7b2 registry.heroku.com

echo.
echo.
echo ==============================================================
echo Docker tag image
echo ==============================================================
echo.
echo.
docker tag bustrackerapi registry.heroku.com/emte-tracker/web
echo Docker tag image is done

echo.
echo.
echo ==============================================================
echo Docker push image to Heroku 
echo ==============================================================
echo.
echo.
docker push registry.heroku.com/emte-tracker/web

echo.
echo.
echo ==============================================================
echo Release
echo ==============================================================
echo.
echo.
heroku container:release web -a emte-tracker

echo ==============================================================
echo ----------------------ALL DONE--------------------------------
echo ==============================================================
PAUSE