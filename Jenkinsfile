pipeline {
    agent any 

    environment {
        HEROKU_API_KEY = credentials('herokuidtest')
    }

    parameters { 
        string(name: 'APP_NAME', defaultValue: '', description: 'What is the Heroku app name?') 
    }

    stages {
        stage('Login') {
            steps {
                bat 'docker login registry.heroku.com -u Thuy Lam -p $HEROKU_API_KEY'
            }
        }

        stage('Push to Heroku registry') {
            steps {
                bat 'docker tag lptest999/docker_backendapi_test registry.heroku.com/$APP_NAME/web'
                bat 'docker push registry.heroku.com/$APP_NAME/web'
            }
        }

        stage('Release the image') {
            steps {
                bat 'heroku container:release web --app=$APP_NAME'
            }
        }
    
        stage('Cleanup') {
            steps {
                deleteDir()
            }
        }
    }
}
