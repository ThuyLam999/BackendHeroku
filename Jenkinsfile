pipeline {
    agent any 
    stages {
        stage('Push to Heroku registry') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'herokuid', passwordVariable: 'password', usernameVariable: 'username')]) {
                    bat 'docker login -u %username% -p %password% registry.heroku.com'
                    bat 'docker tag lptest999/docker_backendapi_test registry.heroku.com/test-api-9/web'
                    bat 'docker push registry.heroku.com/test-api-9/web'
                }
            }          
        }

        stage('Push to Heroku registry') {
            steps {
                withEnv(['PATH+HEROKU=C:\\Program Files\\heroku\\bin']) {
                    bat 'heroku container:release web --app=test-api-9'
                }
            }          
        }
    
        stage('Cleanup') {
            steps {
                deleteDir()
            }
        }
    }
}
