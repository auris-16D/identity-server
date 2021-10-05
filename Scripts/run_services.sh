#!/bin/bash
set -e

export COMMAND="dotnet IdentityServerAspNetIdentity.dll"
export COMMAND="echo 'copying keys' && cp ./certs/* /usr/local/share/ca-certificates && update-ca-certificates --verbose && dotnet IdentityServerAspNetIdentity.dll"

export SENDGRID_KEY="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"
export SENDGRID_USER="NigelSurtees"
if [[ $@ == *"--seed"* ]] ;then
  export COMMAND="echo 'copying keys' && cp ./certs/* /usr/local/share/ca-certificates && update-ca-certificates --verbose && dotnet IdentityServerAspNetIdentity.dll '/seed'"
  # export COMMAND="dotnet IdentityServerAspNetIdentity.dll '/seed'"
echo "Seed is set...."
fi

docker-compose -f docker-compose.yml up
# docker logs -f --since $(date +%s) identity_service
# docker logs -f --since $(date +%s) bdd_api_tests_service
