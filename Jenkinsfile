pipeline {
    agent any 
    stages {
        stage('Push to Heroku registry') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'herokuid', passwordVariable: 'password', usernameVariable: 'username')]) {
                    bat 'docker login registry.heroku.com -u %username% -p %password%'
                    bat 'docker tag lptest999/docker_backendapi_test registry.heroku.com/testapi/web'
                    bat 'docker push registry.heroku.com/testapi/web'
                }  
            }
        }

        stage('Release the image'){
            steps{
                bat 'heroku container:release web --app=testapi'
            }
        }
    
        stage('Cleanup') {
            steps {
                deleteDir()
            }
        }
    }
    post {
        success {
            mail bcc: '', body: 'Thông báo kết quả build', cc: '', from: '', replyTo: '', subject: 'Test Run SUCCESSFUL HEROKU', to: 'thanhthuyyasou234@gmail.com'
        }  

        failure {
            mail bcc: '', body: 'Thông báo kết quả build', cc: '', from: '', replyTo: '', subject: 'Test Run FAILED HEROKU', to: 'thanhthuyyasou234@gmail.com'
        }
    }
}
