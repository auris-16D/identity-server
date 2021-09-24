#!/bin/bash
set -e

    # if [[ $@ == *"--production"* ]] ;then
    # shopt -s nullglob
    #   files=(log/*)
    #   if (( ${#files[*]} )); then
    #     echo 'Removing log files....................'
    #     rm -r log/*
    #   fi
    #   shopt -u nullglob
    #   echo ''
    #   echo '***********************************************************************'
    #   echo '####      Logging in to AWS with pwpr-production credentials      #####'
    #   echo '***********************************************************************'
    #   echo ''
    #   eval "$(aws ecr get-login --profile pwpr-production --region eu-west-1)"
    #   echo ''
    #   echo '*****************************************************************************************************************************************************************************************************************'
    #   echo '          Building the command to run on the container'
    #   command="RAILS_ENV=production bundle exec rake assets:precompile && RAILS_ENV=production bundle && RAILS_ENV=production bundle exec rake db:migrate && RAILS_ENV=production bundle exec rails s -p 3000 -b '0.0.0.0'"
    #   echo $command
    #   echo '*****************************************************************************************************************************************************************************************************************'
    #   echo ''
    #   docker build --build-arg APP_DIR=pwpr --build-arg COMMAND="$command" -t=pwpr .
    #   echo ''
    #   echo ''
    #   echo '***********************************************************************'
    #   echo '                Docker image built, all done......'
    #   echo '***********************************************************************'
    #   echo ''
    # fi

    # if [[ $@ == *"--preprod"* ]] ;then
      shopt -s nullglob
      files=(log/*)
      if (( ${#files[*]} )); then
        echo 'Removing log files....................'
        rm -r log/*
      fi
      shopt -u nullglob
      echo ''
      echo '***********************************************************************'
      echo '####                   Publishing TravelTracker                   #####'
      echo '***********************************************************************'
      dotnet publish './src/Api/Api.csproj' -c Release #--configuration 'Release' --output 'TravelTracker/bin/Release/net5.0/publish'
      dotnet publish './src/IdentityServerAspNetIdentity/IdentityServerAspNetIdentity.csproj' -c Release
    
    #   echo '***********************************************************************'
    #   echo '####      Logging in to AWS with pwpr-preprod credentials         #####'
    #   echo '***********************************************************************'
    #   echo ''
    #   eval "$(aws ecr get-login --profile pwpr-preprod --region eu-west-1)"
    #   echo ''
    #   echo '********************************************************************************************************************************************'
    #   echo '             Building the command to run on the container'
    #   command="RAILS_ENV=preprod bundle && RAILS_ENV=preprod bundle exec rake db:reset && RAILS_ENV=preprod bundle exec rails s -p 3000 -b '0.0.0.0'"
      api_command="dotnet Api.dll"
      echo $api_command
      identity_command="dotnet IdentityServerAspNetIdentity.dll"
      echo $identity_command
      BddApiTests="dotnet test ./app/BddApiTests/BddApiTests.csproj"
      echo $BddApiTests_command
      echo '********************************************************************************************************************************************'
      echo ''
      docker build --build-arg APP_DIR=app --build-arg PORT_NO=6001 --build-arg COMMAND="$api_command" -t api --file "Dockerfile.Api" .
      docker build --build-arg APP_DIR=app --build-arg PORT_NO=5005 --build-arg COMMAND="$identity_command" -t identity --file "Dockerfile.Identity" .
      docker build --build-arg APP_DIR=app --build-arg COMMAND="$BddApiTests_command" -t bdd_api_tests --file "Dockerfile.BddApiTests" .

echo ''
      echo ''
      echo '***********************************************************************'
      echo '                Docker image built, all done......'
      echo '***********************************************************************'
      echo ''
    # fi