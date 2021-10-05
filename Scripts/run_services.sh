#!/bin/bash
set -e

export COMMAND="dotnet IdentityServerAspNetIdentity.dll"
export COMMAND="echo 'copying keys' && cp ./certs/* /usr/local/share/ca-certificates && update-ca-certificates --verbose && dotnet IdentityServerAspNetIdentity.dll"

export SENDGRID_KEY=${SENDGRID_KEY}
export SENDGRID_USER=${SENDGRID_USER}
if [[ $@ == *"--seed"* ]] ;then
  export COMMAND="echo 'copying keys' && cp ./certs/* /usr/local/share/ca-certificates && update-ca-certificates --verbose && dotnet IdentityServerAspNetIdentity.dll '/seed'"
  # export COMMAND="dotnet IdentityServerAspNetIdentity.dll '/seed'"
echo "Seed is set...."
fi

docker-compose -f docker-compose.yml up
# docker logs -f --since $(date +%s) identity_service
# docker logs -f --since $(date +%s) bdd_api_tests_service
